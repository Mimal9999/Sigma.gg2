   M S B A M L   `   `   `  ���� C  ?Sigma.gg, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Sigma.gg.Views.PagesSigma.gg.Views.Pages  1clr-namespace:Sigma.gg.ModelsSigma.gg.Models  3clr-namespace:Sigma.gg.HelpersSigma.gg.Helpers  (    "Sigma.gg.Views.Pages.DashboardPage   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:Sigma.gg.Views.Pages     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     A =Wpf.Ui, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  7	resources'using:AcrylicControls.Example.Resources  *modelsclr-namespace:Sigma.gg.Models  ,helpersclr-namespace:Sigma.gg.Helpers  -     N� Title$  DashboardPage��   ApplicationBackgroundBrush  Wpf.Ui.Markup.Design   
Background# �     TextFillColorPrimaryBrush   
Foreground# �   N� 
Foreground# �   � UseLayoutRounding$
 True�� �� BitmapScalingMode$ HighQuality=� � 	Resources -   'Sigma.gg.Helpers.ChampionImageConverter  ChampionImageConverter)   #Sigma.gg.Helpers.RuneImageConverter  RuneImageConverter2   ,Sigma.gg.Helpers.SummonerSpellImageConverter  SummonerSpellImageConverter)   #Sigma.gg.Helpers.ItemImageConverter  ItemImageConverter#   Sigma.gg.Helpers.KdaConverter  KdaConverter%   Sigma.gg.Helpers.PKillConverter  PKillConverter)   #Sigma.gg.Helpers.ParsedKdaConverter  ParsedKdaConverter(	   "Sigma.gg.Helpers.DmgDealtConverter 	 DmgDealtConverter(
   "Sigma.gg.Helpers.DmgTakenConverter 
 DmgTakenConverter-   'Sigma.gg.Helpers.SecondaryRuneConverter  SecondaryRuneConverter.   (Sigma.gg.Helpers.MvpScoreStringConverter  MvpScoreStringConverter-   'Sigma.gg.Helpers.MvpScoreColorConverter  MvpScoreColorConverter+   %Sigma.gg.Helpers.DivideWidthConverter  DivideWidthConverter(   "Sigma.gg.Helpers.WinColorConverter  WinColorConverter��%�   &	       &	      &	 
     &	      &	      &	      &	      &		 #     &	
 (     &	 -     &	 2     &	 7     &	 <     &	 A                   	  
            .��� $	��Top=� � ColumnDefinitions �� $��* ��� $��* �.+��� $��2q�$
��True��$
��auto=�$��0��$��2������  �� RelativeSource �� 	 �� Mode$	 FindAncestor=�
 �� AncestorType"
 �� �� Path$ ActualHeight�.���  � RowDefinitions �� $��* ��� $��* �.+�  Wpf.Ui.Controls.Button  -   $��1��$��Button��$��100,125,0,0q�$	��Top=�$��95��  -   $��1��$
��Test���� -   $��1��$��Button��$��433,162,0,0q�$	��Top=�$	��169���� -   $��	MatchList��$��1�� %� VirtualizationMode$ 	Recycling=����� *ViewModel.summonerMatches+ �� Mode$ TwoWay=�����  �� 
TargetType" ��.���  �� Property � �� �� Value
 ���� ��  � �� ��  �� 
TargetType" ��.R��� ����  �� DataType   Sigma.gg.Models.MatchData"  .R���  �� CornerRadius$ 2��
�������$��0q�._�� ����  �� Color$ Transparent�� �� Opacity$
 0.75Q� �� $��* ��� $��* ��� $��* � �� $��* ��� $��* �.+���  �� Orientation$ 
Horizontal=�$��Stretch=�$��0��$��0��$��2������ $	 0.7Q� �� $ me.win� �� 	Converter# [ .+�  $
��Left=�$��1q�
�������$��Stretch=��� $ Vertical=�$��20,0,0,0q�$��Center=�.+��� $��Center=�$
��Bold"����� *	queueName+�� $��Center=�$
��Bold"����� *	victory+�� $��Center=�$
��Bold"����� *gameDuration+�� $��Center=�$
��Bold"����� *gameEndString+A� $��50��$��50��$��20,0,0,0q�$��Center=�$
��Left=�����  �� ImageSource �� $ championName�# [ �� $ Vertical=�$��25��$��Center=�$
��Left=�$��5,0,0,0q�.+�  Wpf.Ui.Controls.Image     CornerRadius$ 5��$��0,0,0,5q�   Source �� $ spell1�# [   $ 5�� �� $ spell2�# [ �� $ Vertical=�$��25��$��Center=�$
��Left=�$��3,0,0,0q�.+��� $��0,0,0,6q�$��25��$��25��$��Center=�$ 15�� �� Padding$ 1q����� $ Black��$	 0.5Q�._�  $��Center=�$��Center=� �� $ primaryRune�# [ �� $��0,0,0,2q�$��20��$��20��$��Center=�$ 10��$ 2q����� $ Black��$	 0.5Q�._�  $��Center=�$��Center=� �� $ secondaryRune�# [ �� $ 
Horizontal=�$��35��$��Center=�$��40,0,0,0q�.+��� $��35��$��35��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ itemsIcons[0]�# [ �� $��35��$��35��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ itemsIcons[1]�# [ �� $��35��$��35��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ itemsIcons[2]�# [ �� $��35��$��35��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ itemsIcons[3]�# [ �� $��35��$��35��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ itemsIcons[4]�# [ �� $��35��$��35��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ itemsIcons[5]�# [ A� $��35��$��35��$��0,0,0,0q�$��Center=�$
��Left=�����  �� $ trinket�# [ �� $ Vertical=�$��Center=�$��30,0,0,0q�$��Center=�.+��� 
���  ��$ 10��$	��100��$��0,0,0,0q�._��� $��Center=�$
��Bold"����� *	multikill+�� $��25��$��40��$ 7��$��Center=�$��Center=�$��0,5,0,0q�$ 2q�����  �� $ me.mvpScoreString�# [ ._��� $
��Bold"�$��Center=�$��Center=����� $ me.mvpScoreString�# [ �� $ Vertical=�$��Center=�$��50,0,0,0q�.+��� $��Center=�$
��Bold"���`�  `� 	Converter# [ .1��� $ kills��� $ deaths��� $ assists��� $��Center=�$
��Bold"����� $	 kda�# [ �� $��Center=�$
��Bold"����� *killP+�� $ Vertical=�$��Center=�$��50,0,0,0q�.+��� $��Center=�$
��Bold"����� *controlWards+�� $��Center=�$
��Bold"����� *cs+�� $��Center=�$
��Bold"����� *	avgRank+�� $ 
Horizontal=�$��Right=�$��	0,1,100,1q�$��Center=�$��0��$��1��.+��� $ Vertical=�$��0,0,70,0q�.+��� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[0].championName�# [ �� $��0q�$��Center=����� *participants[0].summonerName+�� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[1].championName�# [ �� $��0q�$��Center=����� *participants[1].summonerName+�� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[2].championName�# [ �� $��0q�$��Center=����� *participants[2].summonerName+�� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[3].championName�# [ �� $��0q�$��Center=����� *participants[3].summonerName+�� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[4].championName�# [ �� $��0q�$��Center=����� *participants[4].summonerName+�� $ Vertical=�.+��� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[5].championName�# [ �� $��0q�$��Center=����� *participants[5].summonerName+�� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[6].championName�# [ �� $��0q�$��Center=����� *participants[6].summonerName+�� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[7].championName�# [ �� $��0q�$��Center=����� *participants[7].summonerName+�� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[8].championName�# [ �� $��0q�$��Center=����� *participants[8].summonerName+�� $ 
Horizontal=�.+�  $ 10��$��0,0,5,2q�$��18��$��Center=� �� $" participants[9].championName�# [ �� $��0q�$��Center=����� *participants[9].summonerName+� $��2��$��1��$��Stretch=�$��Center=�
������ ����  �� ElementName$ 	MatchList��$ ActualWidth� �� $��* ��� $��* �.+��� $��Score��$��12(�$��	118,0,0,0q�$
��Bold"�$��0q�$��0��$
��Left=��� $��Summoner��$��12(�$��	177,0,0,0q�$
��Bold"�$��0q�$��0��$
��Left=��� $	��KDA��$��12(�$��	290,0,0,0q�$
��Bold"�$��0q�$��0��$
��Left=��� $��Damage��$��12(�$��	355,0,0,0q�$
��Bold"�$��0q�$��0��$
��Left=��� $��CS��$��12(�$��	427,0,0,0q�$
��Bold"�$��0q�$��0��$
��Left=��� $��Items��$��12(�$��	560,0,0,0q�$
��Bold"�$��0q�$��0��$
��Left=��� $��Items��$��12(�$��	0,0,560,0q�$
��Bold"�$��0q�$��1��$��Right=��� $��CS��$��12(�$��	0,0,427,0q�$
��Bold"�$��0q�$��1��$��Right=��� $��Damage��$��12(�$��	0,0,355,0q�$
��Bold"�$��0q�$��1��$��Right=��� $	��KDA��$��12(�$��	0,0,290,0q�$
��Bold"�$��0q�$��1��$��Right=��� $��Summoner��$��12(�$��	0,0,177,0q�$
��Bold"�$��0q�$��1��$��Right=��� $��Score��$��12(�$��	0,0,118,0q�$
��Bold"�$��0q�$��1��$��Right=��� $ Vertical=�$��2��$��0��$��Stretch=����� $	 0.7Q� �� $ participantsTeam1[0].win�# [ ���� $ 	MatchList��$ ActualWidth�# [ .+��� $��ParticipantsList��$ 	Recycling=����� *participantsTeam1+$ TwoWay=����� !   Sigma.gg.Models.Participant"  .R��� $ 
Horizontal=�.+�� $��Center=�$
��Left=�$��35��$��35��$��20,0,0,0q�.+�A� $��35��$��35��$��Center=�$
��Left=�����  �� $ championName�# [ �� $ 10��
���iii�$��15��$��15��$
��Left=�$��Bottom=�._��� $��10(�$��Center=�$��Center=����� *
champLevel+�� $ Vertical=�$��20��$��Center=�$
��Left=�$��5,0,0,0q�.+�  $ 5��$��0,0,0,5q� �� $ summoner1Id�# [   $ 5�� �� $ summoner2Id�# [ �� $ Vertical=�$��20��$��Center=�$
��Left=�$��3,0,0,0q�.+��� $��0,0,0,6q�$��20��$��20��$��Center=�$ 10��$ 1q����� $ Black��$	 0.5Q�._�  $��Center=�$��Center=� �� $( "perks.styles[0].selections[0].perk�# [ �� $��0,0,0,2q�$��16��$��16��$��Center=�$ 10��$ 2q����� $ Black��$	 0.5Q�._�  $��Center=�$��Center=� �� $ perks.styles[1].style�# [ �� $��20��$��30��$ 7��$
��Left=�$��Center=�$��10,0,0,0q�$ 2q�����  �� $ mvpScoreString�# [ ._��� $��10(�$
��Bold"�$��Center=�$��Center=����� $ mvpScoreString�# [ �� $ Vertical=�$��10,0,0,0q�$��Center=�.+��� $
��Bold"�$��12(�$��Center=�$	��100��$	��100��$��Center=����� *summonerName+�� $��11(�$��Center=����� *rank+�� $ Vertical=�$��5,0,0,0q�.+��� $
��Bold"�$��12(�$��Center=�$��0q�$��Center=�$��80��$��80����`� # [ .1��� $ kills��� $ deaths��� $ assists��� $��11(�$��Center=�$��0q����� *killParticipation+�� $��11(�$��Center=�$��0q����� $ challenges.kda�# [ �� $ Vertical=�$��10,0,0,0q�$��Center=�.+��� $��12(�$
��Bold"�$��Center=�$��Center=�$��50��$��50������ $! totalDamageDealtToChampions�# [	 �� $��12(�$
��Bold"�$��Center=����� $ totalDamageTaken�# [
 �� $ Vertical=�$��15,0,0,0q�$��Center=�.+��� $��12(�$
��Bold"�$��Center=����� *	totalCs+�� $��12(�$
��Bold"�$��Center=����� *
csPerMin+�� $ 
Horizontal=�$��30��$��Center=�$��15,0,0,0q�.+��� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item0�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item1�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item2�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item3�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item4�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item5�# [ A� $��35��$��35��$��5,0,20,0q�$��Center=�$
��Left=�����  �� $ item6�# [ �� $ Vertical=�$��2��$��1��$��Stretch=����� $	 0.7Q� �� $ participantsTeam2[0].win�# [ ���� $ 	MatchList��$ ActualWidth�# [ .+��� $ 	Recycling=����� *participantsTeam2+$ TwoWay=����� "  .R��� $ 
Horizontal=�$��Right=�.+��� $ 
Horizontal=�$��30��$��Center=�$��20,0,0,0q�.+�A� $��35��$��35��$��0,0,10,0q�$��Center=�$��Right=�����  �� $ item6�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item5�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item4�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item3�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item2�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item1�# [ �� $��30��$��30��$ 5��
�������$��0,0,5,0q�._�  $ 5�� �� $ item0�# [ �� $ Vertical=�$��15,0,0,0q�$��Center=�.+��� $��12(�$
��Bold"�$��Center=����� *	totalCs+�� $��12(�$
��Bold"�$��Center=����� *
csPerMin+�� $ Vertical=�$��15,0,0,0q�$��Center=�.+��� $��12(�$
��Bold"�$��Center=����� $! totalDamageDealtToChampions�# [	 �� $��12(�$
��Bold"�$��Center=����� $ totalDamageTaken�# [
 �� $ Vertical=�$��10,0,0,0q�.+��� $
��Bold"�$��12(�$��Center=�$��0q�$��Center=�$��80��$��80����`� # [ .1��� $ kills��� $ deaths��� $ assists��� $��11(�$��Center=�$��0q�$��Center=����� *killParticipation+�� $��11(�$��Center=�$��0q�$��Center=����� $ challenges.kda�# [ �� $ Vertical=�$��5,0,0,0q�$��Center=�.+��� $
��Bold"�$��12(�$��Center=�$	��100��$	��100��$��Center=����� *summonerName+�� $��11(�$��Center=����� *rank+�� $��20��$��30��$ 7��$��Right=�$��Center=�$��10,0,0,0q�$ 2q�����  �� $ mvpScoreString�# [ ._��� $��10(�$
��Bold"�$��Center=�$��Center=����� $ mvpScoreString�# [ �� $ Vertical=�$��20��$��Center=�$��Right=�$��10,0,0,0q�.+��� $��0,0,0,6q�$��20��$��20��$��Center=�$ 10��$ 1q����� $ Black��$	 0.5Q�._�  $��Center=�$��Center=� �� $( "perks.styles[0].selections[0].perk�# [ �� $��0,0,0,2q�$��16��$��16��$��Center=�$ 10��$ 2q����� $ Black��$	 0.5Q�._�  $��Center=�$��Center=� �� $ perks.styles[1].style�# [ �� $ Vertical=�$��20��$��Center=�$��Right=�$��3,0,0,0q�.+�  $ 5��$��0,0,0,5q� �� $ summoner1Id�# [   $ 5�� �� $ summoner2Id�# [ � $��Center=�$��Right=�$��35��$��35��$��5,0,20,0q�.+�A� $��35��$��35��$��Center=�$��Right=�����  �� $ championName�# [ �� $ 10��
���iii�$��15��$��15��$��Right=�$��Bottom=�._��� $��10(�$��Center=�$��Center=����� *
champLevel+  -   $��Button��$��
289,56,0,0q�$	��Top=�