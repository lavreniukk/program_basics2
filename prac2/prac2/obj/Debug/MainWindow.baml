   M S B A M L   `   `   `  ����@  <prac2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:prac2prac2  5   	       prac2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:prac2     5   	     � Title$  
MainWindow��5   	   $	��500��6   $	��350��6)    � 
ResizeMode$ NoResize=�65    � WindowStartupLocation$ CenterScreen=�6K    � WindowStyle$
 None=�6p    � 	Resources 5	         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background	 �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ExitButtonStyle
 �� 
TargetType �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName  ButtonStyle1��%�  &	        &	 �     &	 �     &	 �     &	     &	 6    &	 Y    &	 |    &	 �    &		 �    &	
 �    7   7  7  7  7  7 	 &	 R    7   7  7  7  7  7 	 �� 5
   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $	 	#FFDDDDDD��6;   �� 5   
   $	 	#FF707070��67   �� 5   
   $	 	#FFBEE6FD��6>   �� 5   
   $	 	#FF3C7FB1��6:   �� 5   
   $	 	#FFC4E5F6��6<   �� 5   
   $	 	#FF2C628B��68   �� 5   
   $	 	#FFF4F4F4��6=   �� 5   
   $	 	#FFADB2B5��69   �� 5   
   $	 	#FF838383��6=   �� 5   
   "
 ��6(   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6+   �� 5       � ��8  6,   �� 5        � ��# � ��6+   �� 5!       � ��$ 1q�60   �� 5"       � ��$ Center=�6;   �� 5#       � ��$ Center=�69   �� 5$       � ��$ 1q�6(   �� 5%       � �� 5&      �� 5'      " ��6&   .R�5(      �� $ border��6!   #��z��61   #��z��6]   #��z��6�    . ._�5)      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5*       5+      P� 5,       �@��IsDefaulted . . �5-   "   �� $ border��6@   # � ��6T    � ��5.      P� 5/       �@J�IsMouseOver . . �50   "   �� $ border��6?    � �� �	#FF1414146S   �� 51   "   $ border��6K    � �� �	#FFBB86FC6_   �� 52   "   $ border��6@    � �� �	#FFBB86FC6T   53      P� 54       � �� . . �55   "   �� $ border��6?    � �� �	#FF1414146S   �� 56   "   $ border��6K    � �� �	#FFBB86FC6_   �� 57   "   $ border��6@    � �� �	#FFBB86FC6T   58      P� 59       � {� .  . �5:   "   �� $ border��6?   8   � ��6S   �� 5;   "   $ border��6@   8   � ��6T   �� 5<   "   $ contentPresenter��6K   8   � ��6i   5=      5?      5A      5B      �� 5C   
   "
 ��6%   .�5D      ��  �@�FocusVisualStyle8   61   �� 5E       � ��8  6+   �� 5F       � ��8  6,   �� 5G       � ��# � ��6+   �� 5H       � ��$ 1q�60   �� 5I       � ��$ Center=�6;   �� 5J       � ��$ Center=�69   �� 5K       � ��$ 1q�6(   �� 5L       � �� 5M      �� 5N      " ��6&   .R�5O      �� $ border��6!   #��z��61   #��z��6]   #��z��6�    . ._�5P      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5Q       5R      P� 5S       �@��IsDefaulted . . �5T   "   �� $ border��6@   # � ��6T    � ��5U      P� 5V       �@J�IsMouseOver . . �5W   "   �� $ border��6?    � �� �#0054576S   �� 5X   "   $ border��6@    � �� �	#FF03DAC66T   5Y      P� 5Z       � �� . . �5[   "   �� $ border��6?    � �� �#0054576S   �� 5\   "   $ border��6@    � �� �	#FF03DAC66T   5]      P� 5^       � {� .  . �5_   "   �� $ border��6?   8   � ��6S   �� 5`   "   $ border��6@   8   � ��6T   �� 5a   "   $ contentPresenter��6K   8   � ��6i   5b      5d      5f      5g      5h      .��5i      � ���	#FF121212.+�5j   
   �  � RowDefinitions 5k      �� 5l      $	��40* �6    �� 5m      $
��170* �6    �� 5n      $	��65* �6    �� 5o      $	��65* �6    �� 5p      $	��55* �6    �� 5q      $
��105* �6    .+�5s      �� ���	#FF1F1F1F$��Center=�6)   $��0��6F   $	��350��6S   $��50��6_   ���	#FF1F1F1F$��Center=�5t   +   $��0,-12,0.4,0.4q�6F   �� 5u      -   5        $
��Name��5u      $
��Left=�6    $��0��6;   $��Center=�6H   $��  Travelling salesman��6c   ���	#FFE1E1E1$��Tahoma)�6�   $��15(�6�   $��	0,5,0,4.4q�6�   $��29��6�   $	��150��6�   �� 5v      -   5        $
��Main��5v      $��1��6    $��Center=�6-   $��Bottom=�6J   $�� Main Menu ��6e   ���	#FFE1E1E1$��Tahoma)�6�   $��30(�6�   $��47��6�   $	��173��6�   �� 5w      -   5        $
��Exit��5w      $��0��6!   $��Right=�6.   $��Center=�6J   $��38��5x      $��38��6!   $	��✖��6B   ���	#FFE1E1E1$��Tahoma)�5y   ?   $��15(�6S   #��� 
 6a   $��0,0,0.4,0.4q�6�   ��5x   ,   Z� ��6N   Z� 5y   �   �� 5z      -   5         ��Name$ OpenGenetic��5z      $��2��6*   $��Center=�67   $��Center=�6T   $��50��5{      $	��200��6!   $�� Open genetic algo ��6-   $��Tahoma)�6K   $��20(�6_   ���	#FF1F1F1F���	#FF1F1F1F���	#FFE1E1E1#��� 
 5}      $��75,0,75.4,2q�6V   �� 5~      -   5        $��
OpenGreedy��5~      $��3��6'   $��Center=�64   $��Center=�6Q   $��50��5      $	��200��6!   $�� Open greedy algo ��6-   $��Tahoma)�6J   $��20(�6^   ���	#FF1F1F1F���	#FF1F1F1F���	#FFE1E1E1#��� 
 5�      6?   �� 5�      -   5        $��Exit2��5�      $��4��6"   $��Center=�6/   $��Center=�6L   $��35��5�      $	��150��6!   $�� Exit ��6-   $��Tahoma)�6>   $��15(�6R   ���	#FF03DAC6���	#00DDDDDD���	#FF03DAC6#���  6�   5�      5�      5�      