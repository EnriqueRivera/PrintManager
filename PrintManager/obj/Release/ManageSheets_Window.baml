   M S B A M L   `   `   `  ����   PrintManager&     PrintManager.ManageSheets_Window   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    -     �Name$  Window�� � Title$ Manage sheets��$��347.578��$	��245�� � 
ResizeMode$ NoResize=� � Icon$ Images/printer.ico�� � 	Resources    BrushWindow �� EndPoint � MappingMode �� 
StartPoint � Color	 � Offset��%t   &	        �� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ Black��$	 1Q�� $ 	#FFDADADA������ $ 0.5,1&�$ 0.5,0&�.��� $ Black��$	 0Q�� $ White��$	 1Q�.��� -   
 �Name$
 
LayoutRoot������ $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF555555��$	 1Q�� $ 	#FFDEDEDE��$	 0.004Q�.+��� -    ��Name$ gpAvailableSheets��$��Available sheets��$
��Left=�$��10,5.331,0,0q�$	��Top=�$��82��$��321.578��$��13.333(�$��Italic$�.��� -   $
 gdControlNumber��$��Center=�$��34.666��$��Center=�$��265.363��.+��� -    ��Name$ lblControlNumber��$��_Control number:��$
��Left=�$��32��$	��Top=�$��93.357��$��0,6.666,0,0q�$��10.667(�$
��Bold"�
 � Tag$ Control number�� �� Target ��  �� ElementName$ txtControlNumber���� -    ��Name$ txtControlNumber��$
��Left=�$��23�� �� TextWrapping$
 Wrap=�$	��Top=�$	��135��$��92.363,5.833,0,0q�
���   � �� TextAlignment$ Center=�$��1��$ idUser���� -    ��Name$ btnSearchUser��$
��Left=�$��231.363,0.777,0,0q�$	��Top=�$��32��$��32�� � Cursor$
 Hand��
���   � � ToolTip$ Search user (Ctrl+ S)��$��2��.���� -    ��Name$ imgSearchUser��$��28��$��Images/search_user.png��$
��Fill=�$��28��$ Search user (Ctrl+ S)���� -	   $ gpAddRemove��$��Add/Remove sheets��$
��Left=�$��8.798,97.967,0,0q�$	��Top=�$��Italic$�$��13.333(�$��322.78��$��108.033��.��� -
   $
 gdAddRemove��$��Center=�$��66.334��$��Center=�$��261.195��.+��� -   $ btnAddSheets��$
��Left=�$��186.195,28.499,0,0q�$	��Top=�$��32��$��32��$
 Hand��
���   �$ Add sheets (Ctrl + A)��$��5��.���� -   $ imgAddSheets��$��Images/addSheets.png��$
��Fill=�$ Add sheets (Ctrl + A)���� -   $ btnRemoveSheets��$
��Left=�$��223.195,28.499,0,0q�$	��Top=�$��32��$��32��$
 Hand��
���   �$ Remove sheets (Ctrl + R)��$��6��.���� -   $ imgRemoveSheets��$��Images/deleteSheets.jpg��$
��Fill=�$ Remove sheets (Ctrl + R)���� -   $ lblNumSheets��$��_Number of sheets:��$
��Left=�$��32��$	��Top=�$��102.19��$��-6,34.001,0,0q�$��10.667(�$
��Bold"�$ Number of sheets�� �� $ txtNumSheets���� -   $ txtNumSheets��$
��Left=�$��23��$
 Wrap=�$	��Top=�$��85.999��$��95.196,33.168,0,0q�
���   �$ Center=�$��4��$ Sheets���� -   $ lblPassword��$��
Pass_word:��$
��Left=�$��32��$	��Top=�$��60.857��$��35.333,2.001,0,0q�$��10.667(�$
��Bold"�$ Password�� �� $ pwdBox��G� -    G�Name$ pwdBox��$
��Left=�$��95.196,0,0,0q�$	��Top=�$��159.999��$��23��
���   �$��3��$ Password��