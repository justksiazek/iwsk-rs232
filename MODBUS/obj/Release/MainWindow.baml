   M S B A M L   `   `   `  ���� =  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Task1;assembly=UITask1  A =MODBUS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Task1.ClassesTask1.Classes ?*clr-namespace:Task1.Converters;assembly=UITask1.Converters  ;(clr-namespace:Task1.Controls;assembly=UITask1.Controls     Task2.MainWindow   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    8x,http://schemas.microsoft.com/winfx/2006/xaml    6dictionariesclr-namespace:Task1;assembly=UI    -classesclr-namespace:Task1.Classes    ?
converters*clr-namespace:Task1.Converters;assembly=UI    ;controls(clr-namespace:Task1.Controls;assembly=UI    $	��690��$	��710��  � Title  �� *Title+ � 	Resources ,   &Task1.Converters.BoolToStringConverter   BoolToStringConverter0   *Task1.Converters.LogTypesToStringConverter  LogTypesToStringConverter9   3Task1.Converters.LogTypedToBackgroundColorConverter & "LogTypedToBackgroundColorConverter-   'Task1.Converters.StringToStopCharacters  StringToStopCharacters  LogItemTemplate �� 
TargetType �� Property �� Value �� 
TargetType �� Orientation �� Path �� 	Converter��%#  &	        &	      &	 
     &	      &	      7  7          �� " ��.���  � �� �� " ��.R�� .+��� $��1,1,1,1q�
���   �$��2,2,2,2q�._��� $ Vertical=�.+��� $
��Bold"����� $ Title�8   ���� $ Title�8  �� ���� $	 Msg�.��� 	 � ColumnDefinitions	 �� $	��31* �$	��450���� $
��410* �
 � RowDefinitions
 �� .+��� $��Odbiór��$��342.5��$��5,10,5,0q�$��0��$��0��$	��Top=�.��*   $Task1.Controls.AutoScrollingListView  #���  $	��314��$	��Top=����� *
LogItems+�� $��	Nadawanie��$��291.5��$��5,0,5,10q�$��0��$��Bottom=�.��� 
 �� $��* ��� $��70 �.+��� $��0�� �� TextWrapping$
 Wrap=����� *	TextInput+ �� UpdateSourceTrigger$ PropertyChanged=�� $��1��.+��� $ 
Horizontal=�$��0,5,0,0q�.+��� $ Vertical=�.+��� $	��211��$��30��$��0,0,5,5q�$'��![M-R1] Transakcja rozgłoszeniowa������ *
WriteAll+�� $��30��$��0,0,5,0q�$"��[M-R1] Transakcja adresowana������ *WriteTransact+�� $ Vertical=�.+��� $	��211��$��30��$��0,0,5,5q�$!��[M-R2] Odczytaj wiadomość������ *RetriveFrom+�� $ 
Horizontal=�.+��� $��Center=�$��Adres stacji Slave���� $	��110��$��25��$��0,0,5,5q�$��Center=����� *SlaveToSend+� $��0��$��1��
 �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+�� $��0��.+��� $��Stacja MODBUS Master��
��������� $��1��$��Konfiguracja stacji��.���� $ Vertical=�.+��� $ 
Horizontal=�.+��� $#��Timeout między znakami ramki��$	��180���� $��50��$��25��$��0,2,0,2q����� *TimeBetweenCharactersTimeout+�� $ 
Horizontal=�.+��� $��Liczba retransmisji��$	��180���� $��50��$��25��$��0,2,0,2q����� *AmountOfRetransmision+�� $��Zastosuj���� $��2��$��Konfiguracja ramki��.��� 	 �� �� 
 �� �� �� �� �� �� �� �� .+��� $��0��$��0��$��Wybór portu���� $��1��$��0��$��5,2,5,2q�$��Center=�$��Center=� �� DisplayMemberPath$ Value�� �� SelectedValuePath$	 Key������ "   Task1.SerialPortDictionaries   NameDictionary �� Source# Z  �� SelectedValue �� *	Port.Name+�� $��0��$��1��$��Szybkość transmisji���� $��1��$��1��$��5,2,5,2q�$��Center=�$��Center=�$ Value��$	 Key������    BaudRateDictionary# Z  �� *Port.BaudRate+�� $��0��$��2��$��Kontrola parzystości���� $��1��$��2��$��5,2,5,2q�$��Center=�$��Center=�$ Value��$	 Key������    ParityDictionary# Z  �� *Port.Parity+�� $��0��$��3��$��Rozmiar pola danych���� $��1��$��3��$��5,2,5,2q�$��Center=�$��Center=�$ Value��$	 Key������    DataBitsDictionary# Z  �� *Port.DataBits+�� $��0��$��4��$��Ilość bitów stopu���� $��1��$��4��$��5,2,5,2q�$��Center=�$��Center=�$ Value��$	 Key������    StopBitsDictionary# Z  �� *Port.StopBits+�� $��0��$��5��$��Kontrola przepływu���� $��1��$��5��$��5,2,5,2q�$��Center=�$��Center=�$ Value��$	 Key������    HandshakeDictionary# Z  �� *Port.Handshake+�� $��0��$��6��$��ReadTimeout���� $��1��$��6��$��5,2,5,2q����� *Port.ReadTimeout+�� $��0��$��7��$��WriteTimeout���� $��1��$��7��$��5,2,5,2q����� *Port.WriteTimeout+� $��3��.+��� $��Stacja MODBUS Slave��
��������� $��4��$��Konfiguracja stacji�� J� RenderTransformOrigin$ 1.232,0.562&�.���� $ Vertical=�.+��� $��stacja pracuje jako Slave��$��0,5,0,3q� d� 	IsChecked �� *	IsSlave+�� $ 
Horizontal=�$��4��.+��� $��Adres stacji��$	��130���� $	��100��$��25��$��0,2,0,2q����� *SlaveAddress+�� $ 
Horizontal=�$��4��.+��� $#��Timeout między znakami ramki��$	��180���� $��50��$��25��$��0,2,0,2q����� *TimeBetweenCharactersTimeout+�� $��Zastosuj��� $��5��.+��� $ Vertical=�.+��� $��
Inne akcje��
��������� $��Zapisz konfigurację��$��5,2,5,2q����� *Reconfigure+�� $��Zamknij port��$��5,2,5,2q����� *	ClosePort+�� $��Otwórz port��$��5,2,5,2q����� *
OpenPort+�� $��Odśwież łącze��$��5,2,5,2q����� *RefreshLines+