   M S B A M L   `   `   `  ����  CourierService*    $CourierService.Views.AddClientWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$%  Добавить клиента��5   	   $	��350��6"   $	��400��6/    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�5   	   ��5      �� 5   
    �� ImageSource$ /Images/back1.png��6    j� Stretch$ UniformToFill=�65   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� $��Имя:��6   $��0��6   $��0��6,   $��Center=�6<   �� 5   
   -   5         ��Name$ FirstNameTextBox��5      $��0��6,   $��1��69   $��5q�6I   �� 5   
   $��Фамилия:��6   $��1��6#   $��0��60   $��Center=�6@   $��0,10,0,0q�6[   �� 5   
   -   5        $ LastNameTextBox��5      $��1��6+   $��1��68   $��5,10,5,0q�6H   �� 5"   
   $��Телефон:��6   $��2��6#   $��0��60   $��Center=�6@   $��0,10,0,0q�6[   �� 5#   
   -   5        $ PhoneTextBox��5#      $��2��6(   $��1��65   $��5,10,5,0q�6E   �� 5&   
   $��Email:��6   $��3��6!   $��0��6.   $��Center=�6>   $��0,10,0,0q�6Y   �� 5'   
   -   5        $ EmailTextBox��5'      $��3��6(   $��1��65   $��5,10,5,0q�6E   �� 5*   
   $��4��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   $��0,20,0,0q�6g   .+�5+      �� -   5        $��Отмена��5+      $��80��6&   $��0,0,10,0q�6L   $��60��6^   �� 5,      -   5        $��Добавить��5,      $��80��6(   $��60��6K   5-      5.      5/      