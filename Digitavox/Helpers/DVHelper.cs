using CommunityToolkit.Mvvm.Messaging.Messages;

///
/// Funções auxiliares
///
/// Author: Jun Okamoto Jr.
/// Date: 30/06/2023
///

namespace Digitavox.Helpers {

  public enum Modifier {
    CapsLock = 0b_0000_0001,
    Shift    = 0b_0000_0010,
    Ctrl     = 0b_0000_0100,
    Alt      = 0b_0000_1000,
    Window   = 0b_0001_0000,
    NumLock  = 0b_0010_0000,
    AltGr    = 0b_0100_0000,
    Fn       = 0b_1000_0000,
    Option   = Alt,
    Command  = Window
  }

  ///
  /// <summary>
  ///   Funções para tratar modificadores de teclas
  ///
  ///   public static void SetXxx(ref int keyModifiers)
  ///   public static void ResetXxx(ref int keyModifiers)
  ///   public static bool IsXxxSet(int keyModifiers)
  ///
  ///   Xxx = CapsLock, Shift, Ctrl, OpttionAlt, Command, NumLock, AltGr, Fn
  ///
  ///   public static void SetModifier(Modifier modifier, ref int keyModifier)
  ///   public static void ResetModifier(Modifier modifier, ref int keyModifier)
  ///   public static bool IsModifierSet(Modifier modifier, int keyModifier)
  /// </summary>
  /// 
  /// <param name="modifier">
  ///   enum Modifier {CapsLock, Shift, Ctrl, Alt, Window, NumLock, AltGr, Fn,
  ///                  Option, Command}
  /// </param>
  ///
  /// <param name="keyModifier">
  ///   bit0 - CapsLock
  ///   bit1 - Shift
  ///   bit2 - Ctrl
  ///   bit3 - Option / Alt
  ///   bit4 - Command / Window
  ///   bit5 - NumLock
  ///   bit6 - AltGr
  ///   bit7 - Fn
  /// </param>
  ///
  public static class DVKeyboard {

    public static void SetModifier(Modifier modifier, ref int keyModifier) {
      keyModifier |= (int)modifier;
    }

    public static void ResetModifier(Modifier modifier, ref int keyModifier) {
      keyModifier &= ~(int)modifier;
    }

    public static bool IsModifierSet(Modifier modifier, int keyModifier) {
      return ((keyModifier & (int)modifier) == (int)modifier);
    }

    public static bool IsModifierKey(string code)
        {
            return Enum.IsDefined(typeof(Modifier), code);
        }

  } // end class DVKeyboard

  /// <summary>
  ///   Funções para detectar tipo de plataforma
  /// </summary>
  /// <returns>
  ///   TRUE se corresponde à plataforma
  ///   FALSE se não corresponde à plataforma
  /// </returns>
  public static class DVDevice {

    public static bool IsAndroid() =>
      DeviceInfo.Current.Platform == DevicePlatform.Android;

    public static bool IsVirtual() =>
      DeviceInfo.Current.DeviceType == DeviceType.Virtual;

    public static bool IsIos() =>
      DeviceInfo.Current.Platform == DevicePlatform.iOS;

    public static bool IsMac() =>
      DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst;

    public static bool IsWindows() =>
      DeviceInfo.Current.Platform == DevicePlatform.WinUI;

  } // end class DVDevice

  /// <summary>
  ///   Classe auxiliar para mensagens para usar com WeakReferenceMessenger
  /// </summary>
  public class DVMessage : ValueChangedMessage<string> {

    public DVMessage(string value) : base(value) { }

  } // end class DVMessage

} // end namespace Helpers

