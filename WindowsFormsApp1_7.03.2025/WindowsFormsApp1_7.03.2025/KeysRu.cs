﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



#region сборка System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Windows.Forms.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

namespace WindowsFormsApp1_7._03._2025
{

    //
    // Сводка:
    //     Задает коды и модификаторы клавиш.
    [Flags]
    [TypeConverter(typeof(KeysConverter))]
    [Editor("System.Windows.Forms.Design.ShortcutKeysEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [ComVisible(true)]
    public enum KeysRu
    {
        //
        // Сводка:
        //     Битовая маска для извлечения кода клавиши из значения клавиши.
        KeyCode = 0xFFFF,
        //
        // Сводка:
        //     Битовая маска для извлечения модификаторов из значения клавиши.
        Modifiers = -65536,
        //
        // Сводка:
        //     Нет нажатых клавиш.
        None = 0x0,
        //
        // Сводка:
        //     Левая кнопка мыши.
        LButton = 0x1,
        //
        // Сводка:
        //     Правая кнопка мыши.
        RButton = 0x2,
        //
        // Сводка:
        //     Клавиша отмены.
        Cancel = 0x3,
        //
        // Сводка:
        //     Средняя кнопка мыши (трехкнопочная мышь).
        MButton = 0x4,
        //
        // Сводка:
        //     Первая кнопка мыши (пятикнопочная мышь).
        XButton1 = 0x5,
        //
        // Сводка:
        //     Вторая кнопка мыши (пятикнопочная мышь).
        XButton2 = 0x6,
        //
        // Сводка:
        //     Клавиша BACKSPACE.
        Back = 0x8,
        //
        // Сводка:
        //     Клавиша TAB.
        Tab = 0x9,
        //
        // Сводка:
        //     Клавиша LINEFEED.
        LineFeed = 0xA,
        //
        // Сводка:
        //     Клавиша CLEAR.
        Clear = 0xC,
        //
        // Сводка:
        //     Клавиша RETURN.
        Return = 0xD,
        //
        // Сводка:
        //     Клавиша ВВОД.
        Enter = 0xD,
        //
        // Сводка:
        //     Клавиша SHIFT.
        ShiftKey = 0x10,
        //
        // Сводка:
        //     Клавиша CTRL.
        ControlKey = 0x11,
        //
        // Сводка:
        //     Клавиша ALT.
        Menu = 0x12,
        //
        // Сводка:
        //     Клавиша PAUSE.
        Pause = 0x13,
        //
        // Сводка:
        //     Клавиша CAPS LOCK.
        Capital = 0x14,
        //
        // Сводка:
        //     Клавиша CAPS LOCK.
        CapsLock = 0x14,
        //
        // Сводка:
        //     Клавиша режима "Кана" редактора метода ввода.
        KanaMode = 0x15,
        //
        // Сводка:
        //     Клавиша режима IME Hanguel (поддерживается для обеспечения совместимости; используйте
        //     клавишу HangulMode).
        HanguelMode = 0x15,
        //
        // Сводка:
        //     Клавиша режима "Хангыль" редактора метода ввода.
        HangulMode = 0x15,
        //
        // Сводка:
        //     Клавиша режима "Джунджа" редактора метода ввода.
        JunjaMode = 0x17,
        //
        // Сводка:
        //     Клавиша окончательного режима IME.
        FinalMode = 0x18,
        //
        // Сводка:
        //     Клавиша режима "Ханджа" редактора метода ввода.
        HanjaMode = 0x19,
        //
        // Сводка:
        //     Клавиша режима "Кандзи" редактора метода ввода.
        KanjiMode = 0x19,
        //
        // Сводка:
        //     Клавиша ESC.
        Escape = 0x1B,
        //
        // Сводка:
        //     Клавиша преобразования IME.
        IMEConvert = 0x1C,
        //
        // Сводка:
        //     Клавиша без преобразования IME.
        IMENonconvert = 0x1D,
        //
        // Сводка:
        //     Клавиша принятия IME, заменяет клавишу System.Windows.Forms.Keys.IMEAceept.
        IMEAccept = 0x1E,
        //
        // Сводка:
        //     Клавиша принятия IME. Является устаревшей, вместо нее используется клавиша System.Windows.Forms.Keys.IMEAccept.
        IMEAceept = 0x1E,
        //
        // Сводка:
        //     Клавиша изменения режима IME.
        IMEModeChange = 0x1F,
        //
        // Сводка:
        //     Клавиша ПРОБЕЛ.
        Space = 0x20,
        //
        // Сводка:
        //     Клавиша PAGE UP.
        Prior = 0x21,
        //
        // Сводка:
        //     Клавиша PAGE UP.
        PageUp = 0x21,
        //
        // Сводка:
        //     Клавиша PAGE DOWN.
        Next = 0x22,
        //
        // Сводка:
        //     Клавиша PAGE DOWN.
        PageDown = 0x22,
        //
        // Сводка:
        //     Клавиша END.
        End = 0x23,
        //
        // Сводка:
        //     Клавиша HOME.
        Home = 0x24,
        //
        // Сводка:
        //     Клавиша СТРЕЛКА ВЛЕВО.
        Left = 0x25,
        //
        // Сводка:
        //     Клавиша СТРЕЛКА ВВЕРХ.
        Up = 0x26,
        //
        // Сводка:
        //     Клавиша СТРЕЛКА ВПРАВО.
        Right = 0x27,
        //
        // Сводка:
        //     Клавиша СТРЕЛКА ВНИЗ.
        Down = 0x28,
        //
        // Сводка:
        //     Клавиша SELECT.
        Select = 0x29,
        //
        // Сводка:
        //     Клавиша PRINT.
        Print = 0x2A,
        //
        // Сводка:
        //     Клавиша EXECUTE.
        Execute = 0x2B,
        //
        // Сводка:
        //     Клавиша PRINT SCREEN.
        Snapshot = 0x2C,
        //
        // Сводка:
        //     Клавиша PRINT SCREEN.
        PrintScreen = 0x2C,
        //
        // Сводка:
        //     Клавиша INS.
        Insert = 0x2D,
        //
        // Сводка:
        //     Клавиша DEL.
        Delete = 0x2E,
        //
        // Сводка:
        //     Клавиша HELP.
        Help = 0x2F,
        //
        // Сводка:
        //     Клавиша 0.
        D0 = 0x30,
        //
        // Сводка:
        //     Клавиша 1.
        D1 = 0x31,
        //
        // Сводка:
        //     Клавиша 2.
        D2 = 0x32,
        //
        // Сводка:
        //     Клавиша 3.
        D3 = 0x33,
        //
        // Сводка:
        //     Клавиша 4.
        D4 = 0x34,
        //
        // Сводка:
        //     Клавиша 5.
        D5 = 0x35,
        //
        // Сводка:
        //     Клавиша 6.
        D6 = 0x36,
        //
        // Сводка:
        //     Клавиша 7.
        D7 = 0x37,
        //
        // Сводка:
        //     Клавиша 8.
        D8 = 0x38,
        //
        // Сводка:
        //     Клавиша 9.
        D9 = 0x39,
        //
        // Сводка:
        //     Клавиша A.
        A = 0x41,
        //
        // Сводка:
        //     Клавиша B.
        B = 0x42,
        //
        // Сводка:
        //     Клавиша C.
        C = 0x43,
        //
        // Сводка:
        //     Клавиша D.
        D = 0x44,
        //
        // Сводка:
        //     Клавиша E.
        E = 0x45,
        //
        // Сводка:
        //     Клавиша F.
        F = 0x46,
        //
        // Сводка:
        //     Клавиша G.
        G = 0x47,
        //
        // Сводка:
        //     Клавиша H.
        H = 0x48,
        //
        // Сводка:
        //     Клавиша I.
        I = 0x49,
        //
        // Сводка:
        //     Клавиша J.
        J = 0x4A,
        //
        // Сводка:
        //     Клавиша K.
        K = 0x4B,
        //
        // Сводка:
        //     Клавиша L.
        L = 0x4C,
        //
        // Сводка:
        //     Клавиша M.
        M = 0x4D,
        //
        // Сводка:
        //     Клавиша N.
        N = 0x4E,
        //
        // Сводка:
        //     Клавиша O.
        O = 0x4F,
        //
        // Сводка:
        //     Клавиша P.
        P = 0x50,
        //
        // Сводка:
        //     Клавиша Q.
        Q = 0x51,
        //
        // Сводка:
        //     Клавиша R.
        R = 0x52,
        //
        // Сводка:
        //     Клавиша S.
        S = 0x53,
        //
        // Сводка:
        //     Клавиша T.
        T = 0x54,
        //
        // Сводка:
        //     Клавиша U.
        U = 0x55,
        //
        // Сводка:
        //     Клавиша V.
        V = 0x56,
        //
        // Сводка:
        //     Клавиша W.
        W = 0x57,
        //
        // Сводка:
        //     Клавиша X.
        X = 0x58,
        //
        // Сводка:
        //     Клавиша Y.
        Y = 0x59,
        //
        // Сводка:
        //     Клавиша Z.
        Z = 0x5A,
        //
        // Сводка:
        //     Левая клавиша с логотипом Windows (клавиатура Microsoft Natural Keyboard).
        LWin = 0x5B,
        //
        // Сводка:
        //     Правая клавиша с логотипом Windows (клавиатура Microsoft Natural Keyboard).
        RWin = 0x5C,
        //
        // Сводка:
        //     Клавиша контекстного меню (клавиатура Microsoft Natural).
        Apps = 0x5D,
        //
        // Сводка:
        //     Клавиша перевода компьютера в спящий режим.
        Sleep = 0x5F,
        //
        // Сводка:
        //     Клавиша 0 на цифровой клавиатуре.
        NumPad0 = 0x60,
        //
        // Сводка:
        //     Клавиша 1 на цифровой клавиатуре.
        NumPad1 = 0x61,
        //
        // Сводка:
        //     Клавиша 2 на цифровой клавиатуре.
        NumPad2 = 0x62,
        //
        // Сводка:
        //     Клавиша 3 на цифровой клавиатуре.
        NumPad3 = 0x63,
        //
        // Сводка:
        //     Клавиша 4 на цифровой клавиатуре.
        NumPad4 = 0x64,
        //
        // Сводка:
        //     Клавиша 5 на цифровой клавиатуре.
        NumPad5 = 0x65,
        //
        // Сводка:
        //     Клавиша 6 на цифровой клавиатуре.
        NumPad6 = 0x66,
        //
        // Сводка:
        //     Клавиша 7 на цифровой клавиатуре.
        NumPad7 = 0x67,
        //
        // Сводка:
        //     Клавиша 8 на цифровой клавиатуре.
        NumPad8 = 0x68,
        //
        // Сводка:
        //     Клавиша 9 на цифровой клавиатуре.
        NumPad9 = 0x69,
        //
        // Сводка:
        //     Клавиша умножения.
        Multiply = 0x6A,
        //
        // Сводка:
        //     Клавиша сложения.
        Add = 0x6B,
        //
        // Сводка:
        //     Клавиша разделителя.
        Separator = 0x6C,
        //
        // Сводка:
        //     Клавиша вычитания.
        Subtract = 0x6D,
        //
        // Сводка:
        //     Клавиша десятичного разделителя.
        Decimal = 0x6E,
        //
        // Сводка:
        //     Клавиша деления.
        Divide = 0x6F,
        //
        // Сводка:
        //     Клавиша F1.
        F1 = 0x70,
        //
        // Сводка:
        //     Клавиша F2.
        F2 = 0x71,
        //
        // Сводка:
        //     Клавиша F3.
        F3 = 0x72,
        //
        // Сводка:
        //     Клавиша F4.
        F4 = 0x73,
        //
        // Сводка:
        //     Клавиша F5.
        F5 = 0x74,
        //
        // Сводка:
        //     Клавиша F6.
        F6 = 0x75,
        //
        // Сводка:
        //     Клавиша F7.
        F7 = 0x76,
        //
        // Сводка:
        //     Клавиша F8.
        F8 = 0x77,
        //
        // Сводка:
        //     Клавиша F9.
        F9 = 0x78,
        //
        // Сводка:
        //     Клавиша F10.
        F10 = 0x79,
        //
        // Сводка:
        //     Клавиша F11.
        F11 = 0x7A,
        //
        // Сводка:
        //     Клавиша F12.
        F12 = 0x7B,
        //
        // Сводка:
        //     Клавиша F13.
        F13 = 0x7C,
        //
        // Сводка:
        //     Клавиша F14.
        F14 = 0x7D,
        //
        // Сводка:
        //     Клавиша F15.
        F15 = 0x7E,
        //
        // Сводка:
        //     Клавиша F16.
        F16 = 0x7F,
        //
        // Сводка:
        //     Клавиша F17.
        F17 = 0x80,
        //
        // Сводка:
        //     Клавиша F18.
        F18 = 0x81,
        //
        // Сводка:
        //     Клавиша F19.
        F19 = 0x82,
        //
        // Сводка:
        //     Клавиша F20.
        F20 = 0x83,
        //
        // Сводка:
        //     Клавиша F21.
        F21 = 0x84,
        //
        // Сводка:
        //     Клавиша F22.
        F22 = 0x85,
        //
        // Сводка:
        //     Клавиша F23.
        F23 = 0x86,
        //
        // Сводка:
        //     Клавиша F24.
        F24 = 0x87,
        //
        // Сводка:
        //     Клавиша NUM LOCK.
        NumLock = 0x90,
        //
        // Сводка:
        //     Клавиша SCROLL LOCK.
        Scroll = 0x91,
        //
        // Сводка:
        //     Левая клавиша SHIFT.
        LShiftKey = 0xA0,
        //
        // Сводка:
        //     Правая клавиша SHIFT.
        RShiftKey = 0xA1,
        //
        // Сводка:
        //     Левая клавиша CTRL.
        LControlKey = 0xA2,
        //
        // Сводка:
        //     Правая клавиша CTRL.
        RControlKey = 0xA3,
        //
        // Сводка:
        //     Левая клавиша ALT.
        LMenu = 0xA4,
        //
        // Сводка:
        //     Правая клавиша ALT.
        RMenu = 0xA5,
        //
        // Сводка:
        //     Клавиша перехода назад в браузере (Windows 2000 или более поздняя версия).
        BrowserBack = 0xA6,
        //
        // Сводка:
        //     Клавиша перехода вперед в браузере (Windows 2000 или более поздняя версия).
        BrowserForward = 0xA7,
        //
        // Сводка:
        //     Клавиша обновления в браузере (Windows 2000 или более поздняя версия).
        BrowserRefresh = 0xA8,
        //
        // Сводка:
        //     Клавиша остановки в браузере (Windows 2000 или более поздняя версия).
        BrowserStop = 0xA9,
        //
        // Сводка:
        //     Клавиша поиска в браузере (Windows 2000 или более поздняя версия).
        BrowserSearch = 0xAA,
        //
        // Сводка:
        //     Клавиша избранного в браузере (Windows 2000 или более поздняя версия).
        BrowserFavorites = 0xAB,
        //
        // Сводка:
        //     Клавиша домашней страницы в браузере (Windows 2000 или более поздняя версия).
        BrowserHome = 0xAC,
        //
        // Сводка:
        //     Клавиша выключения звука (Windows 2000 или более поздняя версия).
        VolumeMute = 0xAD,
        //
        // Сводка:
        //     Клавиша уменьшения громкости (Windows 2000 или более поздняя версия).
        VolumeDown = 0xAE,
        //
        // Сводка:
        //     Клавиша увеличения громкости (Windows 2000 или более поздняя версия).
        VolumeUp = 0xAF,
        //
        // Сводка:
        //     Клавиша перехода на следующую запись (Windows 2000 или более поздняя версия).
        MediaNextTrack = 0xB0,
        //
        // Сводка:
        //     Клавиша перехода на предыдущую запись (Windows 2000 или более поздняя версия).
        MediaPreviousTrack = 0xB1,
        //
        // Сводка:
        //     Клавиша остановки воспроизведения (Windows 2000 или более поздняя версия).
        MediaStop = 0xB2,
        //
        // Сводка:
        //     Клавиша приостановки воспроизведения (Windows 2000 или более поздняя версия).
        MediaPlayPause = 0xB3,
        //
        // Сводка:
        //     Клавиша запуска почты (Windows 2000 или более поздняя версия).
        LaunchMail = 0xB4,
        //
        // Сводка:
        //     Клавиша выбора записи (Windows 2000 или более поздняя версия).
        SelectMedia = 0xB5,
        //
        // Сводка:
        //     Клавиша запуска приложения один (Windows 2000 или более поздняя версия).
        LaunchApplication1 = 0xB6,
        //
        // Сводка:
        //     Клавиша запуска приложения два (Windows 2000 или более поздняя версия).
        LaunchApplication2 = 0xB7,
        //
        // Сводка:
        //     Клавиша OEM с точкой с запятой на стандартной клавиатуре США (Windows 2000 или
        //     более поздняя версия).
        OemSemicolon = 0xBA,
        //
        // Сводка:
        //     Клавиша OEM 1.
        Oem1 = 0xBA,
        //
        // Сводка:
        //     Клавиша OEM со знаком плюса на клавиатуре любой страны или региона (Windows 2000
        //     или более поздняя версия).
        Oemplus = 0xBB,
        //
        // Сводка:
        //     Клавиша OEM с запятой на клавиатуре любой страны или региона (Windows 2000 или
        //     более поздняя версия).
        Oemcomma = 0xBC,
        //
        // Сводка:
        //     Клавиша OEM со знаком минуса на клавиатуре любой страны или региона (Windows
        //     2000 или более поздняя версия).
        OemMinus = 0xBD,
        //
        // Сводка:
        //     Клавиша OEM с точкой на клавиатуре любой страны или региона (Windows 2000 или
        //     более поздняя версия).
        OemPeriod = 0xBE,
        //
        // Сводка:
        //     Клавиша OEM с вопросительным знаком на стандартной клавиатуре США (Windows 2000
        //     или более поздняя версия).
        OemQuestion = 0xBF,
        //
        // Сводка:
        //     Клавиша OEM 2.
        Oem2 = 0xBF,
        //
        // Сводка:
        //     Клавиша OEM со знаком тильды на стандартной клавиатуре США (Windows 2000 или
        //     более поздняя версия).
        Oemtilde = 0xC0,
        //
        // Сводка:
        //     Клавиша OEM 3.
        Oem3 = 0xC0,
        //
        // Сводка:
        //     Клавиша OEM с открывающей квадратной скобкой на стандартной клавиатуре США (Windows
        //     2000 или более поздняя версия).
        OemOpenBrackets = 0xDB,
        //
        // Сводка:
        //     Клавиша OEM 4.
        Oem4 = 0xDB,
        //
        // Сводка:
        //     Клавиша OEM с вертикальной чертой на стандартной клавиатуре США (Windows 2000
        //     или более поздняя версия).
        OemPipe = 0xDC,
        //
        // Сводка:
        //     Клавиша OEM 5.
        Oem5 = 0xDC,
        //
        // Сводка:
        //     Клавиша OEM с закрывающей квадратной скобкой на стандартной клавиатуре США (Windows
        //     2000 или более поздняя версия).
        OemCloseBrackets = 0xDD,
        //
        // Сводка:
        //     Клавиша OEM 6.
        Oem6 = 0xDD,
        //
        // Сводка:
        //     Клавиша OEM с одинарной/двойной кавычкой на стандартной клавиатуре США (Windows
        //     2000 или более поздняя версия).
        OemQuotes = 0xDE,
        //
        // Сводка:
        //     Клавиша OEM 7.
        Oem7 = 0xDE,
        //
        // Сводка:
        //     Клавиша OEM 8.
        Oem8 = 0xDF,
        //
        // Сводка:
        //     Клавиша OEM с угловой скобкой или обратной косой чертой на клавиатуре RT со 102
        //     клавишами (Windows 2000 или более поздняя версия).
        OemBackslash = 0xE2,
        //
        // Сводка:
        //     Клавиша OEM 102.
        Oem102 = 0xE2,
        //
        // Сводка:
        //     Клавиша PROCESS KEY.
        ProcessKey = 0xE5,
        //
        // Сводка:
        //     Используется для передачи символов в Юникоде в виде нажатия клавиш. Значение
        //     клавиши пакета является младшим словом 32-разрядного виртуального значения клавиши,
        //     используемого для бесклавиатурных методов ввода.
        Packet = 0xE7,
        //
        // Сводка:
        //     Клавиша ATTN.
        Attn = 0xF6,
        //
        // Сводка:
        //     Клавиша CRSEL.
        Crsel = 0xF7,
        //
        // Сводка:
        //     Клавиша EXSEL.
        Exsel = 0xF8,
        //
        // Сводка:
        //     Клавиша ERASE EOF.
        EraseEof = 0xF9,
        //
        // Сводка:
        //     Клавиша PLAY.
        Play = 0xFA,
        //
        // Сводка:
        //     Клавиша ZOOM.
        Zoom = 0xFB,
        //
        // Сводка:
        //     Константа, зарезервированная для использования в будущем.
        NoName = 0xFC,
        //
        // Сводка:
        //     Клавиша PA1.
        Pa1 = 0xFD,
        //
        // Сводка:
        //     Клавиша CLEAR.
        OemClear = 0xFE,
        //
        // Сводка:
        //     Клавиша SHIFT.
        Shift = 0x10000,
        //
        // Сводка:
        //     Клавиша CTRL.
        Control = 0x20000,
        //
        // Сводка:
        //     Клавиша ALT.
        Alt = 0x40000
    }
}
#if false // Журнал декомпиляции
Элементов в кэше: "12"
------------------
Разрешить: "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
Найдена одна сборка: "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
Загрузить из: "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
------------------
Разрешить: "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
Найдена одна сборка: "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
Загрузить из: "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
------------------
Разрешить: "System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
Не удалось найти по имени: "System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
------------------
Разрешить: "System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
Найдена одна сборка: "System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
Загрузить из: "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
------------------
Разрешить: "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
Найдена одна сборка: "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
Загрузить из: "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
------------------
Разрешить: "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
Найдена одна сборка: "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
Загрузить из: "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
------------------
Разрешить: "System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
Не удалось найти по имени: "System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
------------------
Разрешить: "Accessibility, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
Не удалось найти по имени: "Accessibility, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
------------------
Разрешить: "System.Deployment, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
Найдена одна сборка: "System.Deployment, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
Загрузить из: "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Deployment.dll"
------------------
Разрешить: "System.Runtime.Serialization.Formatters.Soap, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
Не удалось найти по имени: "System.Runtime.Serialization.Formatters.Soap, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
#endif
