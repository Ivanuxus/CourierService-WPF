   M S B A M L   `   `   `  ����  CourierService*    $CourierService.Views.EditOrderWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  Изменить заказ��5   	   $	��500��6    $	��600��6-   ��5      �� 5   
    �� ImageSource$ /Images/back1.png��6    j� Stretch$ UniformToFill=�65   .��5      �� $��10q�6   .+�5	   
   �� -   5         ��Name$ OrderIdTextBlock��5	      ��6.   �� *	OrderID+�� 5   
    �� Orientation$ 
Horizontal=�6   $��10,0,0,0q�6.   .+�5      �� $��Имя клиента:��6   $	��200��6+   $��Center=�67   �� 5      -   5         ��Name$ ClientComboBox��5      $	��200��6/    �� DisplayMemberPath$ FullName��6;    �� SelectedValuePath$ ClientID��6X   �� 5      -   5        $��+��5      $��40��6!   $��5,0,0,0q�6,   6=   5      �� 5   
   $��Дата заказа:��6   $
��Left=�6'   $	��Top=�6B   $��10,0,0,0q�6Z   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ OrderDatePicker��5      $
��Left=�6.   $	��Top=�6I   $��10,0,0,0q�6a   $	��264��6s   �� 5   
   $"��Описание груза:��6   $
��Left=�6*   $	��Top=�6E   $��10,0,0,0q�6]   $	��264��6o   �� 5   
   -   5        	 ��Name$	 CargoDescriptionTextBox��5      $
��Left=�63   $	��Top=�6N   $��10,0,0,0q�6f   $��30��6x   $	��300��6�   �� 5   
   $ 
Horizontal=�6   $��	10,10,0,0q�6.   .+�5      �� $��Тип груза:��6   $	��200��6)   $��Center=�65   �� 5      -   5        $ CargoTypeComboBox��5      $	��200��62   $ TypeName��6>   $ CargoTypeID��6[   �� 5      -   5        $��+��5      $��40��6!   $��5,0,0,0q�6,   6=   5      �� 5"   
   $ 
Horizontal=�6   $��	10,10,0,0q�6.   .+�5#      �� $��Имя курьера:��6   $	��200��6+   $��Center=�67   �� 5$      -   5        $ CourierComboBox��5$      $	��200��60   $ FullName��6<   $ 	CourierID��6Y   �� 5%      -	   5        $��+��5%      $��40��6!   $��5,0,0,0q�6,   6=   5&      �� 5)   
   $��Базовая цена:��6   $
��Left=�6(   $	��Top=�6C   $��10,0,0,0q�6[   $	��264��6m   �� 5*   
   -
   5        $	 BasePriceTextBox��5*      $
��Left=�6,   $	��Top=�6G   $��10,0,0,0q�6_   $	��264��6q   �� 5,   
   -   5        $��Сохранить��5,      $��Center=�6%   $	��Top=�6B   $��0,30,0,0q�6Z   6l   5-      5.      