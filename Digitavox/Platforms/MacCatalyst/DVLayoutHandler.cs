//using System;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Digitavox.Models;
using Digitavox.ViewModels;
using Digitavox.Helpers;
using CommunityToolkit.Mvvm.Messaging;

namespace Digitavox.PlatformsImplementations
{
    public partial class DVLayoutHandler : LayoutHandler {
    protected override LayoutView CreatePlatformView() {
      return new DVLayoutView();
    }

    protected override void ConnectHandler(LayoutView platformView) {
      platformView.UserInteractionEnabled = true;
      base.ConnectHandler(platformView);
    }
  }
}

public class DVLayoutView : LayoutView {

  WeakReferenceMessenger dvMessenger;

  public DVLayoutView() {
    this.BecomeFirstResponder(); // [jo:230622] faz na inicialização para não precisar de TouchesBegan

    WeakReferenceMessenger.Default.Register<DVMessage>(this, (r, m) => {
      if (m.Value == "BecomeFirstResponder")
        this.BecomeFirstResponder();
    }); // [jo:230831] resolve detecção de teclado no retorno com mensagens
  }

  public override bool CanBecomeFirstResponder {
    get {
      return true;
    }
  }

  public override void TouchesBegan(NSSet touches, UIEvent evt) {
      this.BecomeFirstResponder();
      base.TouchesBegan(touches, evt);
  }

  public override void PressesBegan(NSSet<UIPress> presses, UIPressesEvent evt) {
    //System.Diagnostics.Debug.WriteLine($"PressesBegan {presses.AnyObject.Key.KeyCode.ToString()}");
    base.PressesBegan(presses, evt);
  }

  public override void PressesEnded(NSSet<UIPress> presses, UIPressesEvent evt) {
    //System.Diagnostics.Debug.WriteLine($"PressesEnded {presses.AnyObject.Key.KeyCode.ToString()}");
    //base.PressesEnded(presses, evt);
  }

  public override void PressesCancelled(NSSet<UIPress> presses, UIPressesEvent evt) {
    //System.Diagnostics.Debug.WriteLine($"PressesCancelled {presses.AnyObject.Key.KeyCode.ToString()}");
    // base.PressesCancelled(presses, evt);

    //Console.WriteLine(presses.AnyObject.Key);

    int keyCode = (int)presses.AnyObject.Key.KeyCode;
    int keyModifiers = ((int)presses.AnyObject.Key.ModifierFlags >> 16);

    // [jo:240122] trata ALT GR
    //if (keyCode == 230) { // código da tecla Alt GR
    //  keyModifiers &= ~0x08; // limpa Alt
    //  keyModifiers |= 0x40;  // seta Alt Gr
    //}
    if (keyModifiers == 0x08) // não dá para distinguir Alt Gr com outra tecla
      keyModifiers |= 0x40;   // então seta Alt Gr mesmo que for só Alt

    Console.WriteLine("keyCode: " + keyCode.ToString() + ", keyModifiers: " + keyModifiers.ToString());

    //// para não processar pressesEnd de tecla modifier
    //if (keyCode >= 224) { // 224 é o menor keyCode para tecla modifier (Ctrl, option, command, shift)
    //  base.PressesCancelled(presses, evt);
    //  return;
    //}

    Page p = Shell.Current.CurrentPage;
    if (p is IOnPageKeyPress) {
      bool handled = (p as IOnPageKeyPress).OnPageKeyPress(keyCode, keyModifiers);
      if (!handled)
        base.PressesCancelled(presses, evt);
    } else
      base.PressesCancelled(presses, evt);
  } 
}