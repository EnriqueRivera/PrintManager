   M S B A M L   `   `   `  ����  PrintManager'    !PrintManager.SelectPrinter_Window   5      \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    5      -   5          �Name$  Window��5       � Title$+ %PrintManager V1.0 - Printer selection��5      $��276.333��5      $��77��6    � 
ResizeMode$ NoResize=�6    � Icon$ Images/printer.ico��64    � 	Resources 5         BrushWindow �� EndPoint � MappingMode �� 
StartPoint � Color	 � Offset��%�   &	        �� 5	      $ 0.5,1&�6,   $ RelativeToBoundingBox=�6=   $ 0.5,0&�6a   .��5
      � $ Black��6   $	 1Q�6    � 5      $ 	#FFDADADA��6   5      5      .��5      � -   5        
 �Name$
 
LayoutRoot��5      #���   6   .+�5      �� -   5         ��Name$ cboPrinters��5      $
��Left=�6"   $��10,12.66,0,0q�6=   $	��Top=�6S   $	��214��6k   
���   �
�������
���   � �� SelectedIndex$ 0��6�   $��0��6�   �� 5      -   5         ��Name$ 	btnSelect��5      $��229,9.96,0,0q�6   $
��Bold"�64   $��Italic$�6F   
���   � � Cursor$
 Hand��6m    � ToolTip$ Select (Ctrl + S)��6{   $��28.04��6�   $	��Top=�6�   $
��Left=�6�   $��28.04��6�   $��1q�6�   
���   �$��1��6&  ��5      �� 5      $ 0.5,1&�6   $ 0.5,0&�6+   .��5      � $ White��6   $	 0Q�6"   � 5      $ 	#FF179917��6   $
	 0.75Q�6&   5      .��5      �� -   5         ��Name$ 	imgSelect��5      $��Images/select.png��6   $
��Fill=�69   $ Select (Ctrl + S)��6H   5      5      5      