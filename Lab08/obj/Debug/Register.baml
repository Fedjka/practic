   M S B A M L   `   `   `  ����	  Lab08    Lab08.Register   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  reg_form��5   	    � Title$ Регистрация��5   	   $	��350��6   $	��350��6*    � WindowStartupLocation$ Manual=�66   
������� � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6~   .��5      � .+�5   
   �� -   5         ��Name$ label_Attention��5      $!��Введите данные��6)   $
��Left=�6B   $��	10,10,0,0q�6]   $	��Top=�6p   $	��322��6�   $��42,5,5,5q�6�   
����  �6�   �� 5   
   -   5         ��Name$ textbox_Login��5      $
��Left=�6)   $��41��6D   $��	21,81,0,0q�6P    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��299��6�   $��1,1,1,1q�6�   $��3,5,0,0q�6�   $��16(�6�   $��Italic$�6�   
������� �� TextDecorations 6�   Z� .��6  	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5	      
   Color$
 	#FFD2F1F1��6#   .��5
      5      �� 5   
   $��Логин:��6   $
��Left=�6!   $��	21,50,0,0q�6<   $	��Top=�6O   $��26��6g   $��Normal$�6s   $
��Bold"�6�   �� 5   
   $��Пароль:��6   $
��Left=�6"   $��
21,160,0,0q�6=   $	��Top=�6Q   $��26��6i   $��Normal$�6u   $
��Bold"�6�   �� 5   
   -   5        $ passwordbox_Password��5      $
��Left=�60   $��
21,201,0,0q�6K   $	��Top=�6_   $	��299��6w   $��41��6�   $��1,1,1,1q�6�   $��16(�6�   
�������$��3,5,0,0q�6�   $��Italic$�6�   .��6�   	 5        5      $
 	#FFD2F1F1��6#   .��5      5      �� 5   
   -   5         ��Name$ button_Registration��5      $*��$Зарегистрироваться��6.   $
��Left=�6K   $��Stretch=�6f   $	��120��6�   $��
21,261,0,0q�6�   $��39��6�   
������ J� OpacityMask �	#FF1C96FB � Cursor$
 Hand��6�    �� 	ClickMode$ Press=�6�   6�   �� 5   
   -   5        $ label_Allert��5      $=��7Необходимо заполнить все поля��6&   $
��Left=�6N   $��	70,32,0,0q�6i   $	��Top=�6|   $	��193��6�   
���  ��$y�Hidden=�6�   5      5      