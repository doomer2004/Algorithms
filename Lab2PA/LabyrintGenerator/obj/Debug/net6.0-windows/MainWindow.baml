   M S B A M L   `   `   `  ����L  HLabyrintGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:EconomyViewer.ControlEconomyViewer.Control  5   	   /clr-namespace:LabyrinthMakerLabyrinthMaker  5   	       LabyrinthMaker.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7control#clr-namespace:EconomyViewer.Control     5   	   .localclr-namespace:LabyrinthMaker     5   	     � Title$  
MainWindow��5
   	   $	��750��5   	   $	��600��5   	   $��24(�5   	   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��60 �6   �� 5      $��70 �6   .+�5   
   � -   5         �Name$ 	Main_Grid��5      5      �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�5      $��Center=�5      $��Center=�5      .+�5      �� -   5        $
��Save��5      $��30, 10q�5      $��20(�5      5      �� 5      -   5        $
��Load��5      $��30,10q�5!      $��20(�5"      $
��10,0q�5#      �� 5$      -   5        $��Generate��5$      $��30,10q�5&      $��20(�5'      $
��10,0q�5(      )   #EconomyViewer.Control.IntegerUpDown  5)      -   5          Name$ Size_IUP��5,   $   $��25,0,0,0q�5*   $   $	��100��5+   $      MinValue$ 5��5-   $      MaxValue$
 1111��5.   $      Value 5)   $   �� *Size+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�5.   $   5/      �� 50   
   $��Center=�6   $ 
Horizontal=�51      $��2��52      $��0,15,0,0q�53      $	��440��54      
 � 	Resources
 55       �� 
TargetType �� Property �� Value��%�   '�       �� 56      " �6   .�57      ��  � ��$ 50��58      �� 59       � ��$ 50��5:      �� 5;       � ��$ 30, 0q�5<      -   5        5?      5@      .+�5A      � 
���   �6(   � 5B      
���  ��6&   � 5C      
��� � �6(   � 5D      
�������.+�5E      �� 
���   �$��1,1,1,1q�5F      ._�5G      � 5I      5J      5K      5L      5M      5N      