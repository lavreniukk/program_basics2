#pragma checksum "..\..\ProtectionModeWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "087DA1F8AA1F679C663F3BBC4399B0C72550F78D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Themes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using prac1;


namespace prac1 {
    
    
    /// <summary>
    /// ProtectionModeWindow
    /// </summary>
    public partial class ProtectionModeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 365 "..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock VerifyField;
        
        #line default
        #line hidden
        
        
        #line 369 "..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InputField;
        
        #line default
        #line hidden
        
        
        #line 377 "..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseStudyMode;
        
        #line default
        #line hidden
        
        
        #line 379 "..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SymbolCount;
        
        #line default
        #line hidden
        
        
        #line 382 "..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CountProtection;
        
        #line default
        #line hidden
        
        
        #line 401 "..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label StatisticsBlock;
        
        #line default
        #line hidden
        
        
        #line 416 "..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label P1Field;
        
        #line default
        #line hidden
        
        
        #line 419 "..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label P2Field;
        
        #line default
        #line hidden
        
        
        #line 425 "..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DispField;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/prac1;component/protectionmodewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ProtectionModeWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.VerifyField = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.InputField = ((System.Windows.Controls.TextBox)(target));
            
            #line 372 "..\..\ProtectionModeWindow.xaml"
            this.InputField.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.InputField_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CloseStudyMode = ((System.Windows.Controls.Button)(target));
            
            #line 378 "..\..\ProtectionModeWindow.xaml"
            this.CloseStudyMode.Click += new System.Windows.RoutedEventHandler(this.CloseStudyMode_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SymbolCount = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.CountProtection = ((System.Windows.Controls.ComboBox)(target));
            
            #line 384 "..\..\ProtectionModeWindow.xaml"
            this.CountProtection.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CountProtection_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.StatisticsBlock = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.P1Field = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.P2Field = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.DispField = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

