   M S B A M L   `   `   `  ����@  <Lab08, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab08Lab08  5   	       Lab08.Authoriz   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Lab08     5   	   -   5          �Name$  
login_form��5   "    � Title$ Авторизация��5   	   $	��450��6   $	��450��6*    � 
ResizeMode$ NoResize=�66   .��5	      � $
��Left=�6   $	��421��6&   $��0,0,0,-2q�63   
�������$	��Top=�6X   $	��440��6p    � RowDefinitions 5
   
   �� 5      $	��69* �6   �� 5      $	��67* �6   .+�5   
   �� -   5         ��Name$ button_login��5      $��
Войти��6'   $
��Left=�67   $��
108,37,0,0q�6R   $��1��6f   $	��Top=�6s   $	��220��6�   
���/��� � Cursor$
 Hand��6�   6�   �� 5   
   $��Логин:��6   $
��Left=�6!   $��	91,74,0,0q�6<   $	��Top=�6O   $��26��6g   $��Normal$�6s   $
��Bold"�6�   �� 5   
   $��Пароль:��6   $
��Left=�6"   $��
91,150,0,0q�6=   $	��Top=�6Q   $��26��6i   $��Normal$�6u   $
��Bold"�6�   �� 5   
   -   5        $ 
button_reg��5      $*��$Зарегистрироваться��6%   $
��Left=�6B   $��
108,95,0,0q�6]   $��1��6q   $	��Top=�6~   $	��220��6�   
���/���$
 Hand��6�   6�   �� 5   
   $9��3Пожалуйста авторизируйтесь��6   $
��Left=�65   $��
108,27,0,0q�6P   $	��Top=�6d   $��39��6|   $	��220��6�   $��14(�6�   $��Italic$�6�   $��8,8,5,5q�6�   �� 5   
   -   5         ��Name$ textboxLogin��5      $
��Left=�6(   $��23��6C   $��
91,105,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��259��6�   .��6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      	   
BlurRadius$	 10Q�6#   
   Color$
 	#FFD2F1F1��63   .��5      5      �� 5   
   -   5        $ passwordbox_password��5      $
��Left=�60   $��23��6K   $��
91,181,0,0q�6W   $	��Top=�6k    J� RenderTransformOrigin$ -0.458,0.372&�6�   $	��259��6�    �� SelectionBrush �	#FF084C9E.��6�    5        5      $	 10Q�6#   $
 	#FFD2F1F1��63   .��5      5      �� 5   
   -   5         ��Name$ label_attention��5      $5��/Неверный логин или пароль��6)   $
��Left=�6M   $��	21,54,0,0q�6h   $	��Top=�6{   $	��259��6�   ���	#FFFB0000$��48,5,5,5q�6�   $y�Hidden=�6�   5      5       