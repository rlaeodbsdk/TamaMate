using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransparentWindow : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true)]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("dwmapi.dll")]
    static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMargins);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x80000;
    const int WS_EX_TRANSPARENT = 0x20;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_NOACTIVATE = 0x0010;
    const uint SWP_SHOWWINDOW = 0x0040;

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int left;
        public int right;
        public int top;
        public int bottom;
    }

    private IntPtr hWnd;
    private bool isTransparent = true;

    void Start()
    {
        Application.runInBackground = true;

        hWnd = GetActiveWindow();

        // ������ ���� ���� Ȯ��
        MARGINS margins = new MARGINS() { left = -1, right = -1, top = -1, bottom = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        // Always on top & ��Ȱ�� ���� ����
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0,
            SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);

        // ���� �� Ŭ�� ��� ����
        SetClickThrough(true);
    }

    void Update()
    {
        bool isOverUI = IsPointerOverUI();

        if (isOverUI && isTransparent)
        {
            SetClickThrough(false); // Ŭ�� ����
        }
        else if (!isOverUI && !isTransparent)
        {
            SetClickThrough(true); // Ŭ�� ���
        }
    }

    void SetClickThrough(bool clickThrough)
    {
        int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);

        if (clickThrough)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        else
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, (exStyle | WS_EX_LAYERED) & ~WS_EX_TRANSPARENT);
        }

        isTransparent = clickThrough;
    }

    // Unity ��Ŀ�� ��� �����ϴ� UI ����
    bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0;
    }
}
