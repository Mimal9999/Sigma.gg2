   M S B A M L   `   `   `  ����C  ?Sigma.gg, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Sigma.gg.HelpersSigma.gg.Helpers  5      ;"clr-namespace:Sigma.gg.Views.PagesSigma.gg.Views.Pages  5      '    !Sigma.gg.Views.Pages.SettingsPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2helpersclr-namespace:Sigma.gg.Helpers     5      4local"clr-namespace:Sigma.gg.Views.Pages     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      A =Wpf.Ui, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5	        N� Title$  SettingsPage��5
         ApplicationBackgroundBrush  Wpf.Ui.Markup.Design   
Background# �   5        TextFillColorPrimaryBrush   
Foreground# �  5       N� 
Foreground# �  5       � 	Resources 5      -  'Sigma.gg.Helpers.EnumToBooleanConverter  EnumToBooleanConverter��%   &	         5   
   6)   5      .��5      �� .+�5   
   �� $��20(�5      $��Medium"�5      $��Personalization��5      �� 5   
   $��0,12,0,0q�6   $��Theme��6&   	� 5   
   $��0,12,0,0q�5      $��theme_light��5       $��Light��5!       	� 	GroupName$ themeSelect��5"      ��5      �� *ViewModel.ChangeThemeCommand+ �� Mode$ OneWay=� d� 	IsChecked 5#      �� *ViewModel.CurrentTheme+ �� 	Converter# [ 	 �� ConverterParameter$	 Light��$ OneWay=�	� 5$   
   $��0,8,0,0q�5%      $��
theme_dark��5'      $
��Dark��5(      $ themeSelect��5)      ��5&      �� *ViewModel.ChangeThemeCommand+$ OneWay=� 5*      �� *ViewModel.CurrentTheme+# [ $
	 Dark��$ OneWay=��� 5,   
   $��0,24,0,0q�5-      $��20(�5.      $��Medium"�5/      $��About Sigma.gg��50      �� 51   
   $��0,12,0,0q�6   ��6&   �� *ViewModel.AppVersion+$ OneWay=�52      53      