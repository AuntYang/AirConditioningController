   M S B A M L   `   `   `  ����K  GAir Conditioning, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Air_ConditioningAir_Conditioning  5   	       Air_Conditioning.Login   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Air_Conditioning     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  用户登录��5	   	   $	��450��6   $	��800��6#   .��5
      �  � RowDefinitions 5   
   �� 5      $
��1.2* �6   �� 5      $
��0.8* �6   �� 5      $
��0.8* �6   �� 5      $
��1.4* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��* �6   .+�5   
   �� $��0��5      $��1��6   $��40��5      $	��290��6   $!��欢迎登录空调控制器��5      $��32(�6   $��Center=�5      �� 5   
   -   5        $��getUsername��5       $��1��5!      $��1��6    *  &MaterialDesignOutlinedTextFieldTextBox#��[  5#      $	��Top=�5$      $��40��5%      $	��200��6    �� AcceptsReturn .  �� TextWrapping$
 Wrap=�5'      " �� VerticalScrollBarVisibility$
 Auto=�5(      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入账号��5)      {�. 64   G� 5,   
   -   5        $��getPassword��5-      $��2��5/      $��1��6   $��40��50      $	��200��6    2 .MaterialDesignOutlinedPasswordFieldPasswordBox#��[ 51      $	��Top=�52      {�. $ 请输入密码��54      �� 57   
   -   5        $��3��59      $��1��6   $��40��5:      $	��140��6   $	��Top=�5;        MaterialDesignRaisedButton#��[ 5<      3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�5=         IsIndicatorVisible . 	   IsIndeterminate	 . $��登录��5@      $��20(�6   � 5C   
   -   5        $��ProgressBar��5D      $��3��5E      $��1��6   $��20��5F      $��20��6   $��210,10,10,100q�5G       % !MaterialDesignCircularProgressBar#��[ 5H      
 � Value$
 0Q�5I       � IsIndeterminate .  5K      5M      5N      