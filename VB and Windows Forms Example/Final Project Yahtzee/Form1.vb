Public Class YahtzeeGame
    Dim NumberofPlayers As Integer = 0
    Dim currentplayer As Integer = 1
    Dim DiceNumbers(4) As Integer
    Dim rollcounter As Integer = 0
    Dim RoundCounter As Integer = 1
    Dim MatchCounters(5) As Integer
    Dim TotalofAllDice As Integer = 0
    Dim DiceOneLock As Boolean = False
    Dim DiceTwoLock As Boolean = False
    Dim DiceThreeLock As Boolean = False
    Dim DiceFourLock As Boolean = False
    Dim DiceFiveLock As Boolean = False
    Dim p1ChooseLocks(12) As Boolean
    Dim p2ChooseLocks(12) As Boolean
    Dim p3ChooseLocks(12) As Boolean
    Dim p4ChooseLocks(12) As Boolean
    Dim p1BonusChips As Integer = 0
    Dim p2BonusChips As Integer = 0
    Dim p3BonusChips As Integer = 0
    Dim p4BonusChips As Integer = 0
    Dim p1FinalScore As Integer = 0
    Dim p2FinalScore As Integer = 0
    Dim p3FinalScore As Integer = 0
    Dim p4FinalScore As Integer = 0
    Dim UpperSectionTotals(3) As Integer
    Dim LowerSectionTotals(3) As Integer
    Dim GameHighScore As Integer = 0


    Private Sub btnRollDice_Click(sender As Object, e As EventArgs) Handles btnRollDice.Click
        DiceNumbers = GetDiceNumbers(DiceNumbers)
        setdiceimages(DiceNumbers)
        rollcounter += 1
        If rollcounter = 1 Then
            lblRollNumber.Text = "Roll 1"
            btnEndTurnEarly.Visible = True
        ElseIf rollcounter = 2 Then
            lblRollNumber.Text = "Roll 2"
        ElseIf rollcounter = 3 Then
            LockDice()
            lblRollNumber.Text = "Roll 3"
            btnRollDice.Visible = False
            ActivateButtons(DiceNumbers, currentplayer)
        End If


    End Sub

    Private Sub btnEndTurnEarly_Click(sender As Object, e As EventArgs) Handles btnEndTurnEarly.Click
        LockDice()
        lblRollNumber.Text = "Locked In!"
        btnRollDice.Visible = False
        ActivateButtons(DiceNumbers, currentplayer)
    End Sub

    Private Sub btnNextPlayer_Click(sender As Object, e As EventArgs) Handles btnNextPlayer.Click
        rollcounter = 0
        lblRollNumber.Text = ""
        For x = 0 To 5
            MatchCounters(x) = 0
        Next
        TotalofAllDice = 0
        UnlockDice()
        ClearDiceImages()
        btnRollDice.Visible = True
        btnNextPlayer.Visible = False
        btnEndTurnEarly.Visible = False
        If currentplayer = 1 And NumberofPlayers >= 2 Then
            lblPlayersTurn.Text = "Player 2's Turn,"
            currentplayer += 1
        ElseIf currentplayer = 2 And NumberofPlayers >= 3 Then
            lblPlayersTurn.Text = "Player 3's Turn,"
            currentplayer += 1
        ElseIf currentplayer = 3 And NumberofPlayers = 4 Then
            lblPlayersTurn.Text = "Player 4's Turn,"
            currentplayer += 1
        Else
            If RoundCounter < 13 Then
                RoundCounter += 1
                currentplayer = 1
                lblPlayersTurn.Text = "Player 1's Turn,"
                lblRoundNumber.Text = Convert.ToString(RoundCounter)
            Else
                EndGame()
            End If
        End If

    End Sub

    Public Sub ClearDiceImages()
        For x = 0 To 4
            DiceNumbers(x) = 0
        Next
        setdiceimages(DiceNumbers)
    End Sub


    Private Sub btnSetNumPlayers_Click(sender As Object, e As EventArgs) Handles btnSetNumPlayers.Click
        If txtNumPlayers.Text = "1" Then
            btnRollDice.Visible = True
            lblPlayersTurn.Visible = True
            lblRollNumber.Visible = True
            NumberofPlayers = 1
            txtNumPlayers.ReadOnly = True
            UnlockDice()
            ShowSectionTotals()
        ElseIf txtNumPlayers.Text = "2" Then
            btnRollDice.Visible = True
            lblPlayersTurn.Visible = True
            lblRollNumber.Visible = True
            NumberofPlayers = 2
            txtNumPlayers.ReadOnly = True
            UnlockDice()
            ShowSectionTotals()
        ElseIf txtNumPlayers.Text = "3" Then
            btnRollDice.Visible = True
            lblPlayersTurn.Visible = True
            lblRollNumber.Visible = True
            NumberofPlayers = 3
            txtNumPlayers.ReadOnly = True
            UnlockDice()
            ShowSectionTotals()
        ElseIf txtNumPlayers.Text = "4" Then
            btnRollDice.Visible = True
            lblPlayersTurn.Visible = True
            lblRollNumber.Visible = True
            NumberofPlayers = 4
            txtNumPlayers.ReadOnly = True
            UnlockDice()
            ShowSectionTotals()
        Else
            MsgBox("Players must be between 1 and 4")
            txtNumPlayers.Text = ""
            txtNumPlayers.Focus()
        End If
    End Sub

    Private Sub btnResetGame_Click(sender As Object, e As EventArgs) Handles btnResetGame.Click
        txtNumPlayers.ReadOnly = False
        txtNumPlayers.Focus()
        btnRollDice.Visible = False
        lblPlayersTurn.Visible = False
        lblRollNumber.Visible = False
        btnNextPlayer.Visible = False
        btnEndTurnEarly.Visible = False
        btnSetNumPlayers.Visible = True
        lblPlayer1.Visible = False
        lblPlayer2.Visible = False
        lblPlayer3.Visible = False
        lblPlayer4.Visible = False
        lblPlayer1FinalScore.Visible = False
        lblPlayer2FinalScore.Visible = False
        lblPlayer3FinalScore.Visible = False
        lblPlayer4FinalScore.Visible = False
        lblWinner.Visible = False
        DeactivateAllChoices()
        LockDice()
        rollcounter = 0
        RoundCounter = 1
        p1BonusChips = 0
        p2BonusChips = 0
        p3BonusChips = 0
        p4BonusChips = 0
        currentplayer = 1
        lblPlayersTurn.Text = "Player 1's Turn,"
        lblRollNumber.Text = "Roll 1"
        lblRoundNumber.Text = Convert.ToString(RoundCounter)
        For counter = 0 To 4
            DiceNumbers(counter) = 0
        Next
        setdiceimages(DiceNumbers)
        For x = 0 To 5
            MatchCounters(x) = 0
        Next
        For y = 0 To 12
            p1ChooseLocks(y) = False
            p2ChooseLocks(y) = False
            p3ChooseLocks(y) = False
            p4ChooseLocks(y) = False
        Next
        For z = 0 To 3
            UpperSectionTotals(z) = 0
            LowerSectionTotals(z) = 0
        Next
        GameHighScore = 0
        TotalofAllDice = 0
        ClearScoreCard()

    End Sub


    Public Sub ActivateButtons(Numbers As Integer(), player As Integer)
        Select Case player
            Case 1
                If p1ChooseLocks(0) = False Then
                    OneCheck(Numbers)
                End If
                If p1ChooseLocks(1) = False Then
                    TwoCheck(Numbers)
                End If
                If p1ChooseLocks(2) = False Then
                    ThreeCheck(Numbers)
                End If
                If p1ChooseLocks(3) = False Then
                    FourCheck(Numbers)
                End If
                If p1ChooseLocks(4) = False Then
                    FiveCheck(Numbers)
                End If
                If p1ChooseLocks(5) = False Then
                    SixCheck(Numbers)
                End If

                CountMatches(Numbers)

                If p1ChooseLocks(6) = False Then
                    ThreeoKindCheck(Numbers)
                End If
                If p1ChooseLocks(7) = False Then
                    FouroKindCheck(Numbers)
                End If
                If p1ChooseLocks(8) = False Then
                    FHCheck()
                End If
                If p1ChooseLocks(9) = False Then
                    SStraightCheck(Numbers)
                End If
                If p1ChooseLocks(10) = False Then
                    LStraightCheck(Numbers)
                End If
                If p1ChooseLocks(11) = False Then
                    YahtzeeCheck(Numbers)
                End If
                If p1ChooseLocks(12) = False Then
                    ChanceCheck()
                End If
                CheckForNoChoices()
                ShowBonusButtons(Numbers)
                AddAllDiceNumbers(Numbers)
                btnRollDice.Visible = False
                btnEndTurnEarly.Visible = False
            Case 2
                If p2ChooseLocks(0) = False Then
                    OneCheck(Numbers)
                End If
                If p2ChooseLocks(1) = False Then
                    TwoCheck(Numbers)
                End If
                If p2ChooseLocks(2) = False Then
                    ThreeCheck(Numbers)
                End If
                If p2ChooseLocks(3) = False Then
                    FourCheck(Numbers)
                End If
                If p2ChooseLocks(4) = False Then
                    FiveCheck(Numbers)
                End If
                If p2ChooseLocks(5) = False Then
                    SixCheck(Numbers)
                End If

                CountMatches(Numbers)

                If p2ChooseLocks(6) = False Then
                    ThreeoKindCheck(Numbers)
                End If
                If p2ChooseLocks(7) = False Then
                    FouroKindCheck(Numbers)
                End If
                If p2ChooseLocks(8) = False Then
                    FHCheck()
                End If
                If p2ChooseLocks(9) = False Then
                    SStraightCheck(Numbers)
                End If
                If p2ChooseLocks(10) = False Then
                    LStraightCheck(Numbers)
                End If
                If p2ChooseLocks(11) = False Then
                    YahtzeeCheck(Numbers)
                End If
                If p2ChooseLocks(12) = False Then
                    ChanceCheck()
                End If
                CheckForNoChoices()
                ShowBonusButtons(Numbers)
                AddAllDiceNumbers(Numbers)
                btnRollDice.Visible = False
                btnEndTurnEarly.Visible = False
            Case 3
                If p3ChooseLocks(0) = False Then
                    OneCheck(Numbers)
                End If
                If p3ChooseLocks(1) = False Then
                    TwoCheck(Numbers)
                End If
                If p3ChooseLocks(2) = False Then
                    ThreeCheck(Numbers)
                End If
                If p3ChooseLocks(3) = False Then
                    FourCheck(Numbers)
                End If
                If p3ChooseLocks(4) = False Then
                    FiveCheck(Numbers)
                End If
                If p3ChooseLocks(5) = False Then
                    SixCheck(Numbers)
                End If

                CountMatches(Numbers)

                If p3ChooseLocks(6) = False Then
                    ThreeoKindCheck(Numbers)
                End If
                If p3ChooseLocks(7) = False Then
                    FouroKindCheck(Numbers)
                End If
                If p3ChooseLocks(8) = False Then
                    FHCheck()
                End If
                If p3ChooseLocks(9) = False Then
                    SStraightCheck(Numbers)
                End If
                If p3ChooseLocks(10) = False Then
                    LStraightCheck(Numbers)
                End If
                If p3ChooseLocks(11) = False Then
                    YahtzeeCheck(Numbers)
                End If
                If p3ChooseLocks(12) = False Then
                    ChanceCheck()
                End If
                CheckForNoChoices()
                ShowBonusButtons(Numbers)
                AddAllDiceNumbers(Numbers)
                btnRollDice.Visible = False
                btnEndTurnEarly.Visible = False
            Case 4
                If p4ChooseLocks(0) = False Then
                    OneCheck(Numbers)
                End If
                If p4ChooseLocks(1) = False Then
                    TwoCheck(Numbers)
                End If
                If p4ChooseLocks(2) = False Then
                    ThreeCheck(Numbers)
                End If
                If p4ChooseLocks(3) = False Then
                    FourCheck(Numbers)
                End If
                If p4ChooseLocks(4) = False Then
                    FiveCheck(Numbers)
                End If
                If p4ChooseLocks(5) = False Then
                    SixCheck(Numbers)
                End If

                CountMatches(Numbers)

                If p4ChooseLocks(6) = False Then
                    ThreeoKindCheck(Numbers)
                End If
                If p4ChooseLocks(7) = False Then
                    FouroKindCheck(Numbers)
                End If
                If p4ChooseLocks(8) = False Then
                    FHCheck()
                End If
                If p4ChooseLocks(9) = False Then
                    SStraightCheck(Numbers)
                End If
                If p4ChooseLocks(10) = False Then
                    LStraightCheck(Numbers)
                End If
                If p4ChooseLocks(11) = False Then
                    YahtzeeCheck(Numbers)
                End If
                If p4ChooseLocks(12) = False Then
                    ChanceCheck()
                End If
                CheckForNoChoices()
                ShowBonusButtons(Numbers)
                AddAllDiceNumbers(Numbers)
                btnRollDice.Visible = False
                btnEndTurnEarly.Visible = False
        End Select

    End Sub

    Public Sub ShowBonusButtons(Numbers() As Integer)
        If YahtzeeCheck(Numbers) = True Then
            If currentplayer = 1 Then
                If txtp1Yahtzee.Text = "50" Then
                    lblBonusYahtzee.Visible = True
                    btnJoker.Visible = True
                    btnBonusChip.Visible = True
                ElseIf txtp1Yahtzee.Text = "0" Then
                    lblBonusYahtzee.Visible = True
                    btnJoker.Visible = True
                End If
            End If
            If currentplayer = 2 Then
                If txtp2Yahtzee.Text = "50" Then
                    lblBonusYahtzee.Visible = True
                    btnJoker.Visible = True
                    btnBonusChip.Visible = True
                ElseIf txtp2Yahtzee.Text = "0" Then
                    lblBonusYahtzee.Visible = True
                    btnJoker.Visible = True
                End If
            End If
            If currentplayer = 3 Then
                If txtp3Yahtzee.Text = "50" Then
                    lblBonusYahtzee.Visible = True
                    btnJoker.Visible = True
                    btnBonusChip.Visible = True
                ElseIf txtp3Yahtzee.Text = "0" Then
                    lblBonusYahtzee.Visible = True
                    btnJoker.Visible = True
                End If
            End If
            If currentplayer = 3 Then
                If txtp3Yahtzee.Text = "50" Then
                    lblBonusYahtzee.Visible = True
                    btnJoker.Visible = True
                    btnBonusChip.Visible = True
                ElseIf txtp4Yahtzee.Text = "0" Then
                    lblBonusYahtzee.Visible = True
                    btnJoker.Visible = True
                End If
            End If
        End If
    End Sub


    Public Sub OneCheck(Numbers() As Integer)
        For counter = 0 To 4
            If Numbers(counter) = 1 Then
                btnChooseAces.Visible = True
                Exit For
            End If
        Next
    End Sub

    Public Sub TwoCheck(Numbers() As Integer)
        For counter = 0 To 4
            If Numbers(counter) = 2 Then
                btnChooseTwos.Visible = True
                Exit For
            End If
        Next
    End Sub

    Public Sub ThreeCheck(Numbers() As Integer)
        For counter = 0 To 4
            If Numbers(counter) = 3 Then
                btnChooseThrees.Visible = True
                Exit For
            End If
        Next
    End Sub

    Public Sub FourCheck(Numbers() As Integer)
        For counter = 0 To 4
            If Numbers(counter) = 4 Then
                btnChooseFours.Visible = True
                Exit For
            End If
        Next
    End Sub

    Public Sub FiveCheck(Numbers() As Integer)
        For counter = 0 To 4
            If Numbers(counter) = 5 Then
                btnChooseFives.Visible = True
                Exit For
            End If
        Next
    End Sub

    Public Sub SixCheck(Numbers() As Integer)
        For counter = 0 To 4
            If Numbers(counter) = 6 Then
                btnChooseSixes.Visible = True
                Exit For
            End If
        Next
    End Sub

    Public Sub CountMatches(Numbers() As Integer)
        For x = 0 To 4
            If Numbers(x) = 1 Then
                MatchCounters(0) += 1
            End If
            If Numbers(x) = 2 Then
                MatchCounters(1) += 1
            End If
            If Numbers(x) = 3 Then
                MatchCounters(2) += 1
            End If
            If Numbers(x) = 4 Then
                MatchCounters(3) += 1
            End If
            If Numbers(x) = 5 Then
                MatchCounters(4) += 1
            End If
            If Numbers(x) = 6 Then
                MatchCounters(5) += 1
            End If
        Next
    End Sub

    Public Sub AddAllDiceNumbers(Numbers() As Integer)
        For x = 0 To 4
            TotalofAllDice += Numbers(x)
        Next
    End Sub


    Public Sub ThreeoKindCheck(Numbers() As Integer)
        For x = 0 To 5
            If MatchCounters(x) >= 3 Then
                btnChoose3oKind.Visible = True
                Exit For
            End If
        Next
    End Sub

    Public Sub FouroKindCheck(Numbers() As Integer)
        For x = 0 To 5
            If MatchCounters(x) >= 4 Then
                btnChoose4oKind.Visible = True
                Exit For
            End If
        Next
    End Sub

    Public Sub FHCheck()
        For x = 0 To 5
            If MatchCounters(x) = 3 Then
                For y = 0 To 5
                    If MatchCounters(y) = 2 Then
                        btnChooseFH.Visible = True
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
    End Sub


    Public Sub SStraightCheck(Numbers() As Integer)
        If MatchCounters(0) <> 0 And MatchCounters(1) <> 0 And MatchCounters(2) <> 0 And MatchCounters(3) <> 0 Then
            btnChooseSStraight.Visible = True
        End If

        If MatchCounters(1) <> 0 And MatchCounters(2) <> 0 And MatchCounters(3) <> 0 And MatchCounters(4) <> 0 Then
            btnChooseSStraight.Visible = True
        End If

        If MatchCounters(2) <> 0 And MatchCounters(3) <> 0 And MatchCounters(4) <> 0 And MatchCounters(5) <> 0 Then
            btnChooseSStraight.Visible = True
        End If
    End Sub

    Public Sub LStraightCheck(Numbers() As Integer)
        If MatchCounters(0) <> 0 And MatchCounters(1) <> 0 And MatchCounters(2) <> 0 And MatchCounters(3) <> 0 And MatchCounters(4) <> 0 Then
            btnChooseLStraight.Visible = True
        End If

        If MatchCounters(1) <> 0 And MatchCounters(2) <> 0 And MatchCounters(3) <> 0 And MatchCounters(4) <> 0 And MatchCounters(5) <> 0 Then
            btnChooseLStraight.Visible = True
        End If
    End Sub

    Public Sub CheckForNoChoices()
        If btnChooseChance.Visible = False And btnChooseAces.Visible = False And btnChooseTwos.Visible = False And btnChooseThrees.Visible = False And btnChooseFours.Visible = False And btnChooseFives.Visible = False And btnChooseSixes.Visible = False And btnChoose3oKind.Visible = False And btnChoose4oKind.Visible = False And btnChooseFH.Visible = False And btnChooseSStraight.Visible = False And btnChooseLStraight.Visible = False And btnChooseYahtzee.Visible = False Then
            ShowSet0ButtonsNoChance()
        End If
    End Sub

    Public Sub ChanceCheck()
        If currentplayer = 1 Then
            btnChooseChance.Visible = True
        End If
        If currentplayer = 2 Then
            btnChooseChance.Visible = True
        End If
        If currentplayer = 3 Then
            btnChooseChance.Visible = True
        End If
        If currentplayer = 4 Then
            btnChooseChance.Visible = True
        End If
    End Sub

    Public Function YahtzeeCheck(Numbers() As Integer) As Boolean
        Dim Yahtzee As Boolean = False
        For x = 0 To 5
            If MatchCounters(x) = 5 Then
                Yahtzee = True
                If btnChooseYahtzee.Visible = True Then
                Else
                    btnChooseYahtzee.Visible = True
                    Exit For
                End If
            End If
        Next
        Return Yahtzee
    End Function

    Public Sub SixtyThreeCheck()
        If NumberofPlayers = 1 Then
            If UpperSectionTotals(0) >= 63 Then
                UpperSectionTotals(0) += 35
                txtp1Bonus.Text = "35"
            Else
                txtp1Bonus.Text = "0"
            End If
            ShowSectionTotals()
        End If
        If NumberofPlayers = 2 Then
            If UpperSectionTotals(0) >= 63 Then
                UpperSectionTotals(0) += 35
                txtp1Bonus.Text = "35"
            Else
                txtp1Bonus.Text = "0"
            End If
            If UpperSectionTotals(1) >= 63 Then
                UpperSectionTotals(1) += 35
                txtp2Bonus.Text = "35"
            Else
                txtp2Bonus.Text = "0"
            End If
            ShowSectionTotals()
        End If
        If NumberofPlayers = 3 Then
            If UpperSectionTotals(0) >= 63 Then
                UpperSectionTotals(0) += 35
                txtp1Bonus.Text = "35"
            Else
                txtp1Bonus.Text = "0"
            End If
            If UpperSectionTotals(1) >= 63 Then
                UpperSectionTotals(1) += 35
                txtp2Bonus.Text = "35"
            Else
                txtp2Bonus.Text = "0"
            End If
            If UpperSectionTotals(2) >= 63 Then
                UpperSectionTotals(2) += 35
                txtp3Bonus.Text = "35"
            Else
                txtp3Bonus.Text = "0"
            End If
            ShowSectionTotals()
        End If
        If NumberofPlayers = 4 Then
            If UpperSectionTotals(0) >= 63 Then
                UpperSectionTotals(0) += 35
                txtp1Bonus.Text = "35"
            Else
                txtp1Bonus.Text = "0"
            End If
            If UpperSectionTotals(1) >= 63 Then
                UpperSectionTotals(1) += 35
                txtp2Bonus.Text = "35"
            Else
                txtp2Bonus.Text = "0"
            End If
            If UpperSectionTotals(2) >= 63 Then
                UpperSectionTotals(2) += 35
                txtp3Bonus.Text = "35"
            Else
                txtp3Bonus.Text = "0"
            End If
            If UpperSectionTotals(3) >= 63 Then
                UpperSectionTotals(3) += 35
                txtp4Bonus.Text = "35"
            Else
                txtp4Bonus.Text = "0"
            End If
            ShowSectionTotals()
        End If
    End Sub

    Public Sub EndGame()
        PlaceAllMissingZeros()
        SixtyThreeCheck()

        Dim PlayerScores(3) As Integer
        PlayerScores(0) = UpperSectionTotals(0) + LowerSectionTotals(0)
        PlayerScores(1) = UpperSectionTotals(1) + LowerSectionTotals(1)
        PlayerScores(2) = UpperSectionTotals(2) + LowerSectionTotals(2)
        PlayerScores(3) = UpperSectionTotals(3) + LowerSectionTotals(3)
        If NumberofPlayers = 1 Then
            lblPlayer1FinalScore.Text = Convert.ToString(PlayerScores(0))
        End If
        If NumberofPlayers = 2 Then
            lblPlayer1FinalScore.Text = Convert.ToString(PlayerScores(0))
            lblPlayer2FinalScore.Text = Convert.ToString(PlayerScores(1))
        End If
        If NumberofPlayers = 3 Then
            lblPlayer1FinalScore.Text = Convert.ToString(PlayerScores(0))
            lblPlayer2FinalScore.Text = Convert.ToString(PlayerScores(1))
            lblPlayer3FinalScore.Text = Convert.ToString(PlayerScores(2))
        End If
        If NumberofPlayers = 4 Then
            lblPlayer1FinalScore.Text = Convert.ToString(PlayerScores(0))
            lblPlayer2FinalScore.Text = Convert.ToString(PlayerScores(1))
            lblPlayer3FinalScore.Text = Convert.ToString(PlayerScores(2))
            lblPlayer4FinalScore.Text = Convert.ToString(PlayerScores(3))
        End If

        ShowWinner(PlayerScores)

        If NumberofPlayers = 1 Then
            lblPlayer1.Visible = True
            lblPlayer1FinalScore.Visible = True
        ElseIf NumberofPlayers = 2 Then
            lblPlayer1.Visible = True
            lblPlayer1FinalScore.Visible = True
            lblPlayer2.Visible = True
            lblPlayer2FinalScore.Visible = True
        ElseIf NumberofPlayers = 3 Then
            lblPlayer1.Visible = True
            lblPlayer1FinalScore.Visible = True
            lblPlayer2.Visible = True
            lblPlayer2FinalScore.Visible = True
            lblPlayer3.Visible = True
            lblPlayer3FinalScore.Visible = True
        ElseIf NumberofPlayers = 4 Then
            lblPlayer1.Visible = True
            lblPlayer1FinalScore.Visible = True
            lblPlayer2.Visible = True
            lblPlayer2FinalScore.Visible = True
            lblPlayer3.Visible = True
            lblPlayer3FinalScore.Visible = True
            lblPlayer4.Visible = True
            lblPlayer4FinalScore.Visible = True
        End If
        If Convert.ToInt16(lblHighScore.Text) < GameHighScore Then
            lblHighScore.Text = Convert.ToString(GameHighScore)
        End If
        lblWinner.Visible = True
        LockDice()
        DeactivateAllChoices()
        btnRollDice.Visible = False
        btnNextPlayer.Visible = False
        btnEndTurnEarly.Visible = False
        btnSetNumPlayers.Visible = False
        lblPlayersTurn.Visible = False
    End Sub

    Public Sub ClearScoreCard()
        'txtp1Aces.Text = ""
        'txtp2Aces.Text = ""
        'txtp3Aces.Text = ""
        'txtp4Aces.Text = ""

        'txtp1Twos.Text = ""
        'txtp2Twos.Text = ""
        'txtp3Twos.Text = ""
        'txtp4Twos.Text = ""

        'txtp1Threes.Text = ""
        'txtp2Threes.Text = ""
        'txtp3Threes.Text = ""
        'txtp4Threes.Text = ""

        'txtp1Fours.Text = ""
        'txtp2Fours.Text = ""
        'txtp3Fours.Text = ""
        'txtp4Fours.Text = ""

        'txtp1Fives.Text = ""
        'txtp2Fives.Text = ""
        'txtp3Fives.Text = ""
        'txtp4Fives.Text = ""

        'txtp1Sixes.Text = ""
        'txtp2Sixes.Text = ""
        'txtp3Sixes.Text = ""
        'txtp4Sixes.Text = ""

        'txtp1Bonus.Text = ""
        'txtp2Bonus.Text = ""
        'txtp3Bonus.Text = ""
        'txtp4Bonus.Text = ""

        'txtp1TotalUpper.Text = ""
        'txtp2TotalUpper.Text = ""
        'txtp3TotalUpper.Text = ""
        'txtp4TotalUpper.Text = ""

        'txtp1threeokind.Text = ""
        'txtp2threeokind.Text = ""
        'txtp3threeokind.Text = ""
        'txtp4threeokind.Text = ""

        'txtp1fourokind.Text = ""
        'txtp2fourokind.Text = ""
        'txtp3fourokind.Text = ""
        'txtp4fourokind.Text = ""

        'txtp1FullHouse.Text = ""
        'txtp2FullHouse.Text = ""
        'txtp3FullHouse.Text = ""
        'txtp4FullHouse.Text = ""

        'txtp1SmallStraight.Text = ""
        'txtp2SmallStraight.Text = ""
        'txtp3SmallStraight.Text = ""
        'txtp4SmallStraight.Text = ""

        'txtp1LargeStraight.Text = ""
        'txtp2LargeStraight.Text = ""
        'txtp3LargeStraight.Text = ""
        'txtp4LargeStraight.Text = ""

        'txtp1Chance.Text = ""
        'txtp2Chance.Text = ""
        'txtp3Chance.Text = ""
        'txtp4Chance.Text = ""

        'txtp1Yahtzee.Text = ""
        'txtp2Yahtzee.Text = ""
        'txtp3Yahtzee.Text = ""
        'txtp4Yahtzee.Text = ""

        'txtp1BonusChips.Text = ""
        'txtp2BonusChips.Text = ""
        'txtp3BonusChips.Text = ""
        'txtp4BonusChips.Text = ""

        'txtp1TotalLower.Text = ""
        'txtp2TotalLower.Text = ""
        'txtp3TotalLower.Text = ""
        'txtp4TotalLower.Text = ""

        For Each Control In Controls
            If TypeOf Control Is TextBox Then
                Dim txt As TextBox
                txt = CType(Control, TextBox)
                txt.Clear()
            End If
        Next
    End Sub

    Public Sub PlaceAllMissingZeros()
        If NumberofPlayers = 1 Then
            If p1ChooseLocks(0) = False Then
                txtp1Aces.Text = "0"
            End If
            If p1ChooseLocks(1) = False Then
                txtp1Twos.Text = "0"
            End If
            If p1ChooseLocks(2) = False Then
                txtp1Threes.Text = "0"
            End If
            If p1ChooseLocks(3) = False Then
                txtp1Fours.Text = "0"
            End If
            If p1ChooseLocks(4) = False Then
                txtp1Fives.Text = "0"
            End If
            If p1ChooseLocks(5) = False Then
                txtp1Sixes.Text = "0"
            End If
            If p1ChooseLocks(6) = False Then
                txtp1threeokind.Text = "0"
            End If
            If p1ChooseLocks(7) = False Then
                txtp1fourokind.Text = "0"
            End If
            If p1ChooseLocks(8) = False Then
                txtp1FullHouse.Text = "0"
            End If
            If p1ChooseLocks(9) = False Then
                txtp1SmallStraight.Text = "0"
            End If
            If p1ChooseLocks(10) = False Then
                txtp1LargeStraight.Text = "0"
            End If
            If p1ChooseLocks(12) = False Then
                txtp1Chance.Text = "0"
            End If
            If p1ChooseLocks(11) = False Then
                txtp1Yahtzee.Text = "0"
            End If
            If txtp1BonusChips.Text = "" Then
                txtp1BonusChips.Text = "0"
            End If
        End If
        If NumberofPlayers = 2 Then
            If p1ChooseLocks(0) = False Then
                txtp1Aces.Text = "0"
            End If
            If p1ChooseLocks(1) = False Then
                txtp1Twos.Text = "0"
            End If
            If p1ChooseLocks(2) = False Then
                txtp1Threes.Text = "0"
            End If
            If p1ChooseLocks(3) = False Then
                txtp1Fours.Text = "0"
            End If
            If p1ChooseLocks(4) = False Then
                txtp1Fives.Text = "0"
            End If
            If p1ChooseLocks(5) = False Then
                txtp1Sixes.Text = "0"
            End If
            If p1ChooseLocks(6) = False Then
                txtp1threeokind.Text = "0"
            End If
            If p1ChooseLocks(7) = False Then
                txtp1fourokind.Text = "0"
            End If
            If p1ChooseLocks(8) = False Then
                txtp1FullHouse.Text = "0"
            End If
            If p1ChooseLocks(9) = False Then
                txtp1SmallStraight.Text = "0"
            End If
            If p1ChooseLocks(10) = False Then
                txtp1LargeStraight.Text = "0"
            End If
            If p1ChooseLocks(12) = False Then
                txtp1Chance.Text = "0"
            End If
            If p1ChooseLocks(11) = False Then
                txtp1Yahtzee.Text = "0"
            End If
            If txtp1BonusChips.Text = "" Then
                txtp1BonusChips.Text = "0"
            End If
            If p2ChooseLocks(0) = False Then
                txtp2Aces.Text = "0"
            End If
            If p2ChooseLocks(2) = False Then
                txtp2Twos.Text = "0"
            End If
            If p2ChooseLocks(2) = False Then
                txtp2Threes.Text = "0"
            End If
            If p2ChooseLocks(3) = False Then
                txtp2Fours.Text = "0"
            End If
            If p2ChooseLocks(4) = False Then
                txtp2Fives.Text = "0"
            End If
            If p2ChooseLocks(5) = False Then
                txtp2Sixes.Text = "0"
            End If
            If p2ChooseLocks(6) = False Then
                txtp2threeokind.Text = "0"
            End If
            If p2ChooseLocks(7) = False Then
                txtp2fourokind.Text = "0"
            End If
            If p2ChooseLocks(8) = False Then
                txtp2FullHouse.Text = "0"
            End If
            If p2ChooseLocks(9) = False Then
                txtp2SmallStraight.Text = "0"
            End If
            If p2ChooseLocks(10) = False Then
                txtp2LargeStraight.Text = "0"
            End If
            If p2ChooseLocks(12) = False Then
                txtp2Chance.Text = "0"
            End If
            If p2ChooseLocks(11) = False Then
                txtp2Yahtzee.Text = "0"
            End If
            If txtp2BonusChips.Text = "" Then
                txtp2BonusChips.Text = "0"
            End If
        End If
        If NumberofPlayers = 3 Then
            If p1ChooseLocks(0) = False Then
                txtp1Aces.Text = "0"
            End If
            If p1ChooseLocks(1) = False Then
                txtp1Twos.Text = "0"
            End If
            If p1ChooseLocks(2) = False Then
                txtp1Threes.Text = "0"
            End If
            If p1ChooseLocks(3) = False Then
                txtp1Fours.Text = "0"
            End If
            If p1ChooseLocks(4) = False Then
                txtp1Fives.Text = "0"
            End If
            If p1ChooseLocks(5) = False Then
                txtp1Sixes.Text = "0"
            End If
            If p1ChooseLocks(6) = False Then
                txtp1threeokind.Text = "0"
            End If
            If p1ChooseLocks(7) = False Then
                txtp1fourokind.Text = "0"
            End If
            If p1ChooseLocks(8) = False Then
                txtp1FullHouse.Text = "0"
            End If
            If p1ChooseLocks(9) = False Then
                txtp1SmallStraight.Text = "0"
            End If
            If p1ChooseLocks(10) = False Then
                txtp1LargeStraight.Text = "0"
            End If
            If p1ChooseLocks(12) = False Then
                txtp1Chance.Text = "0"
            End If
            If p1ChooseLocks(11) = False Then
                txtp1Yahtzee.Text = "0"
            End If
            If txtp1BonusChips.Text = "" Then
                txtp1BonusChips.Text = "0"
            End If
            If p2ChooseLocks(0) = False Then
                txtp2Aces.Text = "0"
            End If
            If p2ChooseLocks(2) = False Then
                txtp2Twos.Text = "0"
            End If
            If p2ChooseLocks(2) = False Then
                txtp2Threes.Text = "0"
            End If
            If p2ChooseLocks(3) = False Then
                txtp2Fours.Text = "0"
            End If
            If p2ChooseLocks(4) = False Then
                txtp2Fives.Text = "0"
            End If
            If p2ChooseLocks(5) = False Then
                txtp2Sixes.Text = "0"
            End If
            If p2ChooseLocks(6) = False Then
                txtp2threeokind.Text = "0"
            End If
            If p2ChooseLocks(7) = False Then
                txtp2fourokind.Text = "0"
            End If
            If p2ChooseLocks(8) = False Then
                txtp2FullHouse.Text = "0"
            End If
            If p2ChooseLocks(9) = False Then
                txtp2SmallStraight.Text = "0"
            End If
            If p2ChooseLocks(10) = False Then
                txtp2LargeStraight.Text = "0"
            End If
            If p2ChooseLocks(12) = False Then
                txtp2Chance.Text = "0"
            End If
            If p2ChooseLocks(11) = False Then
                txtp2Yahtzee.Text = "0"
            End If
            If txtp2BonusChips.Text = "" Then
                txtp2BonusChips.Text = "0"
            End If
            If p3ChooseLocks(0) = False Then
                txtp3Aces.Text = "0"
            End If
            If p3ChooseLocks(2) = False Then
                txtp3Twos.Text = "0"
            End If
            If p3ChooseLocks(2) = False Then
                txtp3Threes.Text = "0"
            End If
            If p3ChooseLocks(3) = False Then
                txtp3Fours.Text = "0"
            End If
            If p3ChooseLocks(4) = False Then
                txtp3Fives.Text = "0"
            End If
            If p3ChooseLocks(5) = False Then
                txtp3Sixes.Text = "0"
            End If
            If p3ChooseLocks(6) = False Then
                txtp3threeokind.Text = "0"
            End If
            If p3ChooseLocks(7) = False Then
                txtp3fourokind.Text = "0"
            End If
            If p3ChooseLocks(8) = False Then
                txtp3FullHouse.Text = "0"
            End If
            If p3ChooseLocks(9) = False Then
                txtp3SmallStraight.Text = "0"
            End If
            If p3ChooseLocks(10) = False Then
                txtp3LargeStraight.Text = "0"
            End If
            If p3ChooseLocks(12) = False Then
                txtp3Chance.Text = "0"
            End If
            If p3ChooseLocks(11) = False Then
                txtp3Yahtzee.Text = "0"
            End If
            If txtp3BonusChips.Text = "" Then
                txtp3BonusChips.Text = "0"
            End If
        End If
        If NumberofPlayers = 4 Then
            If p1ChooseLocks(0) = False Then
                txtp1Aces.Text = "0"
            End If
            If p1ChooseLocks(1) = False Then
                txtp1Twos.Text = "0"
            End If
            If p1ChooseLocks(2) = False Then
                txtp1Threes.Text = "0"
            End If
            If p1ChooseLocks(3) = False Then
                txtp1Fours.Text = "0"
            End If
            If p1ChooseLocks(4) = False Then
                txtp1Fives.Text = "0"
            End If
            If p1ChooseLocks(5) = False Then
                txtp1Sixes.Text = "0"
            End If
            If p1ChooseLocks(6) = False Then
                txtp1threeokind.Text = "0"
            End If
            If p1ChooseLocks(7) = False Then
                txtp1fourokind.Text = "0"
            End If
            If p1ChooseLocks(8) = False Then
                txtp1FullHouse.Text = "0"
            End If
            If p1ChooseLocks(9) = False Then
                txtp1SmallStraight.Text = "0"
            End If
            If p1ChooseLocks(10) = False Then
                txtp1LargeStraight.Text = "0"
            End If
            If p1ChooseLocks(12) = False Then
                txtp1Chance.Text = "0"
            End If
            If p1ChooseLocks(11) = False Then
                txtp1Yahtzee.Text = "0"
            End If
            If txtp1BonusChips.Text = "" Then
                txtp1BonusChips.Text = "0"
            End If
            If p2ChooseLocks(0) = False Then
                txtp2Aces.Text = "0"
            End If
            If p2ChooseLocks(2) = False Then
                txtp2Twos.Text = "0"
            End If
            If p2ChooseLocks(2) = False Then
                txtp2Threes.Text = "0"
            End If
            If p2ChooseLocks(3) = False Then
                txtp2Fours.Text = "0"
            End If
            If p2ChooseLocks(4) = False Then
                txtp2Fives.Text = "0"
            End If
            If p2ChooseLocks(5) = False Then
                txtp2Sixes.Text = "0"
            End If
            If p2ChooseLocks(6) = False Then
                txtp2threeokind.Text = "0"
            End If
            If p2ChooseLocks(7) = False Then
                txtp2fourokind.Text = "0"
            End If
            If p2ChooseLocks(8) = False Then
                txtp2FullHouse.Text = "0"
            End If
            If p2ChooseLocks(9) = False Then
                txtp2SmallStraight.Text = "0"
            End If
            If p2ChooseLocks(10) = False Then
                txtp2LargeStraight.Text = "0"
            End If
            If p2ChooseLocks(12) = False Then
                txtp2Chance.Text = "0"
            End If
            If p2ChooseLocks(11) = False Then
                txtp2Yahtzee.Text = "0"
            End If
            If txtp2BonusChips.Text = "" Then
                txtp2BonusChips.Text = "0"
            End If
            If p3ChooseLocks(0) = False Then
                txtp3Aces.Text = "0"
            End If
            If p3ChooseLocks(2) = False Then
                txtp3Twos.Text = "0"
            End If
            If p3ChooseLocks(2) = False Then
                txtp3Threes.Text = "0"
            End If
            If p3ChooseLocks(3) = False Then
                txtp3Fours.Text = "0"
            End If
            If p3ChooseLocks(4) = False Then
                txtp3Fives.Text = "0"
            End If
            If p3ChooseLocks(5) = False Then
                txtp3Sixes.Text = "0"
            End If
            If p3ChooseLocks(6) = False Then
                txtp3threeokind.Text = "0"
            End If
            If p3ChooseLocks(7) = False Then
                txtp3fourokind.Text = "0"
            End If
            If p3ChooseLocks(8) = False Then
                txtp3FullHouse.Text = "0"
            End If
            If p3ChooseLocks(9) = False Then
                txtp3SmallStraight.Text = "0"
            End If
            If p3ChooseLocks(10) = False Then
                txtp3LargeStraight.Text = "0"
            End If
            If p3ChooseLocks(12) = False Then
                txtp3Chance.Text = "0"
            End If
            If p3ChooseLocks(11) = False Then
                txtp3Yahtzee.Text = "0"
            End If
            If txtp3BonusChips.Text = "" Then
                txtp3BonusChips.Text = "0"
            End If
            If p4ChooseLocks(0) = False Then
                txtp4Aces.Text = "0"
            End If
            If p4ChooseLocks(2) = False Then
                txtp4Twos.Text = "0"
            End If
            If p4ChooseLocks(2) = False Then
                txtp4Threes.Text = "0"
            End If
            If p4ChooseLocks(3) = False Then
                txtp4Fours.Text = "0"
            End If
            If p4ChooseLocks(4) = False Then
                txtp4Fives.Text = "0"
            End If
            If p4ChooseLocks(5) = False Then
                txtp4Sixes.Text = "0"
            End If
            If p4ChooseLocks(6) = False Then
                txtp4threeokind.Text = "0"
            End If
            If p4ChooseLocks(7) = False Then
                txtp4fourokind.Text = "0"
            End If
            If p4ChooseLocks(8) = False Then
                txtp4FullHouse.Text = "0"
            End If
            If p4ChooseLocks(9) = False Then
                txtp4SmallStraight.Text = "0"
            End If
            If p4ChooseLocks(10) = False Then
                txtp4LargeStraight.Text = "0"
            End If
            If p4ChooseLocks(12) = False Then
                txtp4Chance.Text = "0"
            End If
            If p4ChooseLocks(11) = False Then
                txtp4Yahtzee.Text = "0"
            End If
            If txtp4BonusChips.Text = "" Then
                txtp4BonusChips.Text = "0"
            End If
        End If
    End Sub


    Public Function GetDiceNumbers(Numbers As Integer()) As Integer()
        Dim rnd As Random = New Random()
        If DiceOneLock = False Then
            Numbers(0) = rnd.Next(1, 7)
        End If
        If DiceTwoLock = False Then
            Numbers(1) = rnd.Next(1, 7)
        End If
        If DiceThreeLock = False Then
            Numbers(2) = rnd.Next(1, 7)
        End If
        If DiceFourLock = False Then
            Numbers(3) = rnd.Next(1, 7)
        End If
        If DiceFiveLock = False Then
            Numbers(4) = rnd.Next(1, 7)
        End If
        Return Numbers
    End Function


    Public Sub setdiceimages(Numbers As Integer())
        pboxDice1.Image = getimage(Numbers(0))
        pboxDice2.Image = getimage(Numbers(1))
        pboxDice3.Image = getimage(Numbers(2))
        pboxDice4.Image = getimage(Numbers(3))
        pboxDice5.Image = getimage(Numbers(4))

    End Sub

    Public Function getimage(Number As Integer) As Image

        Dim picture As Image

        Select Case Number
            Case 1
                picture = Global.Final_Project_Yahtzee.My.Resources.Resources.dice1
            Case 2
                picture = Global.Final_Project_Yahtzee.My.Resources.Resources.dice2
            Case 3
                picture = Global.Final_Project_Yahtzee.My.Resources.Resources.dice3
            Case 4
                picture = Global.Final_Project_Yahtzee.My.Resources.Resources.dice4
            Case 5
                picture = Global.Final_Project_Yahtzee.My.Resources.Resources.dice5
            Case 6
                picture = Global.Final_Project_Yahtzee.My.Resources.Resources.dice6
            Case Else
                picture = Global.Final_Project_Yahtzee.My.Resources.Resources.Red_X
        End Select

        Return picture

    End Function

    Private Sub btnDice1Lock_Click(sender As Object, e As EventArgs) Handles btnDice1Lock.Click
        DiceOneLock = True
        btnDice1Lock.Visible = False
    End Sub

    Private Sub btnDice2Lock_Click(sender As Object, e As EventArgs) Handles btnDice2Lock.Click
        DiceTwoLock = True
        btnDice2Lock.Visible = False
    End Sub

    Private Sub btnDice3Lock_Click(sender As Object, e As EventArgs) Handles btnDice3Lock.Click
        DiceThreeLock = True
        btnDice3Lock.Visible = False
    End Sub

    Private Sub btnDice4Lock_Click(sender As Object, e As EventArgs) Handles btnDice4Lock.Click
        DiceFourLock = True
        btnDice4Lock.Visible = False
    End Sub

    Private Sub btnDice5Lock_Click(sender As Object, e As EventArgs) Handles btnDice5Lock.Click
        DiceFiveLock = True
        btnDice5Lock.Visible = False
    End Sub

    Public Sub UnlockDice()
        DiceOneLock = False
        DiceTwoLock = False
        DiceThreeLock = False
        DiceFourLock = False
        DiceFiveLock = False
        btnDice1Lock.Visible = True
        btnDice2Lock.Visible = True
        btnDice3Lock.Visible = True
        btnDice4Lock.Visible = True
        btnDice5Lock.Visible = True
    End Sub

    Public Sub LockDice()
        DiceOneLock = True
        DiceTwoLock = True
        DiceThreeLock = True
        DiceFourLock = True
        DiceFiveLock = True
        btnDice1Lock.Visible = False
        btnDice2Lock.Visible = False
        btnDice3Lock.Visible = False
        btnDice4Lock.Visible = False
        btnDice5Lock.Visible = False
    End Sub

    Public Sub ShowSectionTotals()
        If NumberofPlayers = 1 Then
            txtp1TotalUpper.Text = Convert.ToString(UpperSectionTotals(0))
            txtp1TotalLower.Text = Convert.ToString(LowerSectionTotals(0))
        End If
        If NumberofPlayers = 2 Then
            txtp1TotalUpper.Text = Convert.ToString(UpperSectionTotals(0))
            txtp1TotalLower.Text = Convert.ToString(LowerSectionTotals(0))
            txtp2TotalUpper.Text = Convert.ToString(UpperSectionTotals(1))
            txtp2TotalLower.Text = Convert.ToString(LowerSectionTotals(1))
        End If
        If NumberofPlayers = 3 Then
            txtp1TotalUpper.Text = Convert.ToString(UpperSectionTotals(0))
            txtp1TotalLower.Text = Convert.ToString(LowerSectionTotals(0))
            txtp2TotalUpper.Text = Convert.ToString(UpperSectionTotals(1))
            txtp2TotalLower.Text = Convert.ToString(LowerSectionTotals(1))
            txtp3TotalUpper.Text = Convert.ToString(UpperSectionTotals(2))
            txtp3TotalLower.Text = Convert.ToString(LowerSectionTotals(2))
        End If
        If NumberofPlayers = 4 Then
            txtp1TotalUpper.Text = Convert.ToString(UpperSectionTotals(0))
            txtp1TotalLower.Text = Convert.ToString(LowerSectionTotals(0))
            txtp2TotalUpper.Text = Convert.ToString(UpperSectionTotals(1))
            txtp2TotalLower.Text = Convert.ToString(LowerSectionTotals(1))
            txtp3TotalUpper.Text = Convert.ToString(UpperSectionTotals(2))
            txtp3TotalLower.Text = Convert.ToString(LowerSectionTotals(2))
            txtp4TotalUpper.Text = Convert.ToString(UpperSectionTotals(3))
            txtp4TotalLower.Text = Convert.ToString(LowerSectionTotals(3))
        End If
    End Sub


    Private Sub btnBonusChip_Click(sender As Object, e As EventArgs) Handles btnBonusChip.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            p1BonusChips += 1
            txtp1BonusChips.Text = Convert.ToString(p1BonusChips)
            LowerSectionTotals(0) += 100
        End If

        If currentplayer = 2 Then
            p2BonusChips += 1
            txtp2BonusChips.Text = Convert.ToString(p2BonusChips)
            LowerSectionTotals(1) += 100
        End If

        If currentplayer = 3 Then
            p3BonusChips += 1
            txtp3BonusChips.Text = Convert.ToString(p3BonusChips)
            LowerSectionTotals(2) += 100
        End If

        If currentplayer = 4 Then
            p4BonusChips += 1
            txtp4BonusChips.Text = Convert.ToString(p4BonusChips)
            LowerSectionTotals(3) += 100
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnJoker_Click(sender As Object, e As EventArgs) Handles btnJoker.Click
        lblAddYahtzeeTotalTo.Visible = True
        ShowAddYahtzeeTotalButtons()
        btnChooseAces.Visible = False
        btnChooseTwos.Visible = False
        btnChooseThrees.Visible = False
        btnChooseFours.Visible = False
        btnChooseFives.Visible = False
        btnChooseSixes.Visible = False
        btnChoose3oKind.Visible = False
        btnChoose4oKind.Visible = False
        btnChooseFH.Visible = False
        btnChooseSStraight.Visible = False
        btnChooseLStraight.Visible = False
        btnChooseChance.Visible = False
        btnChooseYahtzee.Visible = False
        btnPlaceZeroAces.Visible = False
        btnPlaceZeroTwos.Visible = False
        btnPlaceZeroThrees.Visible = False
        btnPlaceZeroFours.Visible = False
        btnPlaceZeroFives.Visible = False
        btnPlaceZeroSixes.Visible = False
        btnPlaceZero3oKind.Visible = False
        btnPlaceZero4oKind.Visible = False
        btnPlaceZeroFH.Visible = False
        btnPlaceZeroSStraight.Visible = False
        btnPlaceZeroLStraight.Visible = False
        btnPlaceZeroYahtzee.Visible = False
        lblBonusYahtzee.Visible = False
        btnBonusChip.Visible = False
        btnJoker.Visible = False
    End Sub

    Public Sub ShowSet0ButtonsNoChance()
        If currentplayer = 1 Then
            If p1ChooseLocks(0) = False Then
                btnPlaceZeroAces.Visible = True
            End If
            If p1ChooseLocks(1) = False Then
                btnPlaceZeroTwos.Visible = True
            End If
            If p1ChooseLocks(2) = False Then
                btnPlaceZeroThrees.Visible = True
            End If
            If p1ChooseLocks(3) = False Then
                btnPlaceZeroFours.Visible = True
            End If
            If p1ChooseLocks(4) = False Then
                btnPlaceZeroFives.Visible = True
            End If
            If p1ChooseLocks(5) = False Then
                btnPlaceZeroSixes.Visible = True
            End If
            If p1ChooseLocks(6) = False Then

                btnPlaceZero3oKind.Visible = True
            End If
            If p1ChooseLocks(7) = False Then
                btnPlaceZero4oKind.Visible = True
            End If
            If p1ChooseLocks(8) = False Then
                btnPlaceZeroFH.Visible = True
            End If
            If p1ChooseLocks(9) = False Then
                btnPlaceZeroSStraight.Visible = True
            End If
            If p1ChooseLocks(10) = False Then
                btnPlaceZeroLStraight.Visible = True
            End If
            If p1ChooseLocks(11) = False Then
                btnPlaceZeroYahtzee.Visible = True
            End If
        End If
        If currentplayer = 2 Then
            If p2ChooseLocks(0) = False Then
                btnPlaceZeroAces.Visible = True
            End If
            If p2ChooseLocks(1) = False Then
                btnPlaceZeroTwos.Visible = True
            End If
            If p2ChooseLocks(2) = False Then
                btnPlaceZeroThrees.Visible = True
            End If
            If p2ChooseLocks(3) = False Then
                btnPlaceZeroFours.Visible = True
            End If
            If p2ChooseLocks(4) = False Then
                btnPlaceZeroFives.Visible = True
            End If
            If p2ChooseLocks(5) = False Then
                btnPlaceZeroSixes.Visible = True
            End If
            If p2ChooseLocks(6) = False Then
                btnPlaceZero3oKind.Visible = True
            End If
            If p2ChooseLocks(7) = False Then
                btnPlaceZero4oKind.Visible = True
            End If
            If p2ChooseLocks(8) = False Then
                btnPlaceZeroFH.Visible = True
            End If
            If p2ChooseLocks(9) = False Then
                btnPlaceZeroSStraight.Visible = True
            End If
            If p2ChooseLocks(10) = False Then
                btnPlaceZeroLStraight.Visible = True
            End If
            If p2ChooseLocks(11) = False Then
                btnPlaceZeroYahtzee.Visible = True
            End If
        End If
        If currentplayer = 3 Then
            If p3ChooseLocks(0) = False Then
                btnPlaceZeroAces.Visible = True
            End If
            If p3ChooseLocks(1) = False Then
                btnPlaceZeroTwos.Visible = True
            End If
            If p3ChooseLocks(2) = False Then
                btnPlaceZeroThrees.Visible = True
            End If
            If p3ChooseLocks(3) = False Then
                btnPlaceZeroFours.Visible = True
            End If
            If p3ChooseLocks(4) = False Then
                btnPlaceZeroFives.Visible = True
            End If
            If p3ChooseLocks(5) = False Then
                btnPlaceZeroSixes.Visible = True
            End If
            If p3ChooseLocks(6) = False Then
                btnPlaceZero3oKind.Visible = True
            End If
            If p3ChooseLocks(7) = False Then
                btnPlaceZero4oKind.Visible = True
            End If
            If p3ChooseLocks(8) = False Then
                btnPlaceZeroFH.Visible = True
            End If
            If p3ChooseLocks(9) = False Then
                btnPlaceZeroSStraight.Visible = True
            End If
            If p3ChooseLocks(10) = False Then
                btnPlaceZeroLStraight.Visible = True
            End If
            If p3ChooseLocks(11) = False Then
                btnPlaceZeroYahtzee.Visible = True
            End If
        End If
        If currentplayer = 4 Then
            If p4ChooseLocks(0) = False Then
                btnPlaceZeroAces.Visible = True
            End If
            If p4ChooseLocks(1) = False Then
                btnPlaceZeroTwos.Visible = True
            End If
            If p4ChooseLocks(2) = False Then
                btnPlaceZeroThrees.Visible = True
            End If
            If p4ChooseLocks(3) = False Then
                btnPlaceZeroFours.Visible = True
            End If
            If p4ChooseLocks(4) = False Then
                btnPlaceZeroFives.Visible = True
            End If
            If p4ChooseLocks(5) = False Then
                btnPlaceZeroSixes.Visible = True
            End If
            If p4ChooseLocks(6) = False Then
                btnPlaceZero3oKind.Visible = True
            End If
            If p4ChooseLocks(7) = False Then
                btnPlaceZero4oKind.Visible = True
            End If
            If p4ChooseLocks(8) = False Then
                btnPlaceZeroFH.Visible = True
            End If
            If p4ChooseLocks(9) = False Then
                btnPlaceZeroSStraight.Visible = True
            End If
            If p4ChooseLocks(10) = False Then
                btnPlaceZeroLStraight.Visible = True
            End If
            If p4ChooseLocks(11) = False Then
                btnPlaceZeroYahtzee.Visible = True
            End If
        End If

    End Sub

    Public Sub ShowAddYahtzeeTotalButtons()
        If txtp1threeokind.Text <> "" And txtp1fourokind.Text <> "" And txtp1FullHouse.Text <> "" And txtp1SmallStraight.Text <> "" And txtp1LargeStraight.Text <> "" Then
            If currentplayer = 1 Then
                If txtp1Aces.Text = "" Then
                    btnAddYTotalAces.Visible = True
                End If
                If txtp1Twos.Text = "" Then
                    btnAddYTotalTwos.Visible = True
                End If
                If txtp1Threes.Text = "" Then
                    btnAddYTotalThrees.Visible = True
                End If
                If txtp1Fours.Text = "" Then
                    btnAddYTotalFours.Visible = True
                End If
                If txtp1Fives.Text = "" Then
                    btnAddYTotalFives.Visible = True
                End If
                If txtp1Sixes.Text = "" Then
                    btnAddYTotalSixes.Visible = True
                End If
            End If
            If currentplayer = 2 Then
                If txtp2Aces.Text = "" Then
                    btnAddYTotalAces.Visible = True
                End If
                If txtp2Twos.Text = "" Then
                    btnAddYTotalTwos.Visible = True
                End If
                If txtp2Threes.Text = "" Then
                    btnAddYTotalThrees.Visible = True
                End If
                If txtp2Fours.Text = "" Then
                    btnAddYTotalFours.Visible = True
                End If
                If txtp2Fives.Text = "" Then
                    btnAddYTotalFives.Visible = True
                End If
                If txtp2Sixes.Text = "" Then
                    btnAddYTotalSixes.Visible = True
                End If
            End If
            If currentplayer = 3 Then
                If txtp3Aces.Text = "" Then
                    btnAddYTotalAces.Visible = True
                End If
                If txtp3Twos.Text = "" Then
                    btnAddYTotalTwos.Visible = True
                End If
                If txtp3Threes.Text = "" Then
                    btnAddYTotalThrees.Visible = True
                End If
                If txtp3Fours.Text = "" Then
                    btnAddYTotalFours.Visible = True
                End If
                If txtp3Fives.Text = "" Then
                    btnAddYTotalFives.Visible = True
                End If
                If txtp3Sixes.Text = "" Then
                    btnAddYTotalSixes.Visible = True
                End If
            End If
            If currentplayer = 4 Then
                If txtp4Aces.Text = "" Then
                    btnAddYTotalAces.Visible = True
                End If
                If txtp4Twos.Text = "" Then
                    btnAddYTotalTwos.Visible = True
                End If
                If txtp4Threes.Text = "" Then
                    btnAddYTotalThrees.Visible = True
                End If
                If txtp4Fours.Text = "" Then
                    btnAddYTotalFours.Visible = True
                End If
                If txtp4Fives.Text = "" Then
                    btnAddYTotalFives.Visible = True
                End If
                If txtp4Sixes.Text = "" Then
                    btnAddYTotalSixes.Visible = True
                End If
            End If
        Else
            If currentplayer = 1 Then
                If txtp1threeokind.Text = "" Then
                    btnAddYTotal3oKind.Visible = True
                End If
                If txtp1fourokind.Text = "" Then
                    btnAddYTotal4oKind.Visible = True
                End If
                If txtp1FullHouse.Text = "" Then
                    btnAddYTotalFH.Visible = True
                End If
                If txtp1SmallStraight.Text = "" Then
                    btnAddYTotalSStraight.Visible = True
                End If
                If txtp1LargeStraight.Text = "" Then
                    btnAddYTotalLStraight.Visible = True
                End If
            End If
            If currentplayer = 2 Then
                If txtp2threeokind.Text = "" Then
                    btnAddYTotal3oKind.Visible = True
                End If
                If txtp2fourokind.Text = "" Then
                    btnAddYTotal4oKind.Visible = True
                End If
                If txtp2FullHouse.Text = "" Then
                    btnAddYTotalFH.Visible = True
                End If
                If txtp2SmallStraight.Text = "" Then
                    btnAddYTotalSStraight.Visible = True
                End If
                If txtp2LargeStraight.Text = "" Then
                    btnAddYTotalLStraight.Visible = True
                End If
            End If
            If currentplayer = 3 Then
                If txtp3threeokind.Text = "" Then
                    btnAddYTotal3oKind.Visible = True
                End If
                If txtp3fourokind.Text = "" Then
                    btnAddYTotal4oKind.Visible = True
                End If
                If txtp3FullHouse.Text = "" Then
                    btnAddYTotalFH.Visible = True
                End If
                If txtp3SmallStraight.Text = "" Then
                    btnAddYTotalSStraight.Visible = True
                End If
                If txtp3LargeStraight.Text = "" Then
                    btnAddYTotalLStraight.Visible = True
                End If
            End If
            If currentplayer = 4 Then
                If txtp4threeokind.Text = "" Then
                    btnAddYTotal3oKind.Visible = True
                End If
                If txtp4fourokind.Text = "" Then
                    btnAddYTotal4oKind.Visible = True
                End If
                If txtp4FullHouse.Text = "" Then
                    btnAddYTotalFH.Visible = True
                End If
                If txtp4SmallStraight.Text = "" Then
                    btnAddYTotalSStraight.Visible = True
                End If
                If txtp4LargeStraight.Text = "" Then
                    btnAddYTotalLStraight.Visible = True
                End If
            End If

        End If
    End Sub

    Public Sub DeactivateAllChoices()
        btnChooseAces.Visible = False
        btnChooseTwos.Visible = False
        btnChooseThrees.Visible = False
        btnChooseFours.Visible = False
        btnChooseFives.Visible = False
        btnChooseSixes.Visible = False
        btnChoose3oKind.Visible = False
        btnChoose4oKind.Visible = False
        btnChooseFH.Visible = False
        btnChooseSStraight.Visible = False
        btnChooseLStraight.Visible = False
        btnChooseChance.Visible = False
        btnChooseYahtzee.Visible = False
        btnPlaceZeroAces.Visible = False
        btnPlaceZeroTwos.Visible = False
        btnPlaceZeroThrees.Visible = False
        btnPlaceZeroFours.Visible = False
        btnPlaceZeroFives.Visible = False
        btnPlaceZeroSixes.Visible = False
        btnPlaceZero3oKind.Visible = False
        btnPlaceZero4oKind.Visible = False
        btnPlaceZeroFH.Visible = False
        btnPlaceZeroSStraight.Visible = False
        btnPlaceZeroLStraight.Visible = False
        btnPlaceZeroYahtzee.Visible = False
        lblBonusYahtzee.Visible = False
        btnBonusChip.Visible = False
        btnJoker.Visible = False
        lblAddYahtzeeTotalTo.Visible = False
        btnAddYTotalAces.Visible = False
        btnAddYTotalTwos.Visible = False
        btnAddYTotalFours.Visible = False
        btnAddYTotalFives.Visible = False
        btnAddYTotalSixes.Visible = False
        btnAddYTotal3oKind.Visible = False
        btnAddYTotal4oKind.Visible = False
        btnAddYTotalFH.Visible = False
        btnAddYTotalSStraight.Visible = False
        btnAddYTotalLStraight.Visible = False
    End Sub

    Private Sub btnChooseAces_Click(sender As Object, e As EventArgs) Handles btnChooseAces.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Aces.Text = Convert.ToString(MatchCounters(0))
            p1ChooseLocks(0) = True
            UpperSectionTotals(0) += MatchCounters(0)
        End If
        If currentplayer = 2 Then
            txtp2Aces.Text = Convert.ToString(MatchCounters(0))
            p2ChooseLocks(0) = True
            UpperSectionTotals(1) += MatchCounters(0)
        End If
        If currentplayer = 3 Then
            txtp3Aces.Text = Convert.ToString(MatchCounters(0))
            p2ChooseLocks(0) = True
            UpperSectionTotals(2) += MatchCounters(0)
        End If
        If currentplayer = 4 Then
            txtp4Aces.Text = Convert.ToString(MatchCounters(0))
            p2ChooseLocks(1) = True
            UpperSectionTotals(3) += MatchCounters(0)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnChooseTwos_Click(sender As Object, e As EventArgs) Handles btnChooseTwos.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Twos.Text = Convert.ToString(MatchCounters(1) * 2)
            p1ChooseLocks(1) = True
            UpperSectionTotals(0) += (MatchCounters(1) * 2)
        End If
        If currentplayer = 2 Then
            txtp2Twos.Text = Convert.ToString(MatchCounters(1) * 2)
            p2ChooseLocks(1) = True
            UpperSectionTotals(1) += (MatchCounters(1) * 2)
        End If
        If currentplayer = 3 Then
            txtp3Twos.Text = Convert.ToString(MatchCounters(1) * 2)
            p3ChooseLocks(1) = True
            UpperSectionTotals(2) += (MatchCounters(1) * 2)
        End If
        If currentplayer = 4 Then
            txtp4Twos.Text = Convert.ToString(MatchCounters(1) * 2)
            p4ChooseLocks(1) = True
            UpperSectionTotals(3) += (MatchCounters(1) * 2)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnChooseThrees_Click(sender As Object, e As EventArgs) Handles btnChooseThrees.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Threes.Text = Convert.ToString(MatchCounters(2) * 3)
            p1ChooseLocks(2) = True
            UpperSectionTotals(0) += (MatchCounters(2) * 3)
        End If
        If currentplayer = 2 Then
            txtp2Threes.Text = Convert.ToString(MatchCounters(2) * 3)
            p2ChooseLocks(2) = True
            UpperSectionTotals(1) += (MatchCounters(2) * 3)
        End If
        If currentplayer = 3 Then
            txtp3Threes.Text = Convert.ToString(MatchCounters(2) * 3)
            p3ChooseLocks(2) = True
            UpperSectionTotals(2) += (MatchCounters(2) * 3)
        End If
        If currentplayer = 4 Then
            txtp4Threes.Text = Convert.ToString(MatchCounters(2) * 3)
            p4ChooseLocks(2) = True
            UpperSectionTotals(3) += (MatchCounters(2) * 3)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnChooseFours_Click(sender As Object, e As EventArgs) Handles btnChooseFours.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Fours.Text = Convert.ToString(MatchCounters(3) * 4)
            p1ChooseLocks(3) = True
            UpperSectionTotals(0) += (MatchCounters(3) * 4)
        End If
        If currentplayer = 2 Then
            txtp2Fours.Text = Convert.ToString(MatchCounters(3) * 4)
            p2ChooseLocks(3) = True
            UpperSectionTotals(1) += (MatchCounters(3) * 4)
        End If
        If currentplayer = 3 Then
            txtp3Fours.Text = Convert.ToString(MatchCounters(3) * 4)
            p3ChooseLocks(3) = True
            UpperSectionTotals(2) += (MatchCounters(3) * 4)
        End If
        If currentplayer = 4 Then
            txtp4Fours.Text = Convert.ToString(MatchCounters(3) * 4)
            p4ChooseLocks(3) = True
            UpperSectionTotals(3) += (MatchCounters(3) * 4)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnChooseFives_Click(sender As Object, e As EventArgs) Handles btnChooseFives.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Fives.Text = Convert.ToString(MatchCounters(4) * 5)
            p1ChooseLocks(4) = True
            UpperSectionTotals(0) += (MatchCounters(4) * 5)
        End If
        If currentplayer = 2 Then
            txtp2Fives.Text = Convert.ToString(MatchCounters(4) * 5)
            p2ChooseLocks(4) = True
            UpperSectionTotals(1) += (MatchCounters(4) * 5)
        End If
        If currentplayer = 3 Then
            txtp3Fives.Text = Convert.ToString(MatchCounters(4) * 5)
            p3ChooseLocks(4) = True
            UpperSectionTotals(2) += (MatchCounters(4) * 5)
        End If
        If currentplayer = 4 Then
            txtp4Fives.Text = Convert.ToString(MatchCounters(4) * 5)
            p4ChooseLocks(4) = True
            UpperSectionTotals(3) += (MatchCounters(4) * 5)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnChooseSixes_Click(sender As Object, e As EventArgs) Handles btnChooseSixes.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Sixes.Text = Convert.ToString(MatchCounters(5) * 6)
            p1ChooseLocks(5) = True
            UpperSectionTotals(0) += (MatchCounters(5) * 6)
        End If
        If currentplayer = 2 Then
            txtp2Sixes.Text = Convert.ToString(MatchCounters(5) * 6)
            p2ChooseLocks(5) = True
            UpperSectionTotals(1) += (MatchCounters(5) * 6)
        End If
        If currentplayer = 3 Then
            txtp3Sixes.Text = Convert.ToString(MatchCounters(5) * 6)
            p3ChooseLocks(5) = True
            UpperSectionTotals(2) += (MatchCounters(5) * 6)
        End If
        If currentplayer = 4 Then
            txtp4Sixes.Text = Convert.ToString(MatchCounters(5) * 6)
            p4ChooseLocks(5) = True
            UpperSectionTotals(3) += (MatchCounters(5) * 6)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnChoose3oKind_Click(sender As Object, e As EventArgs) Handles btnChoose3oKind.Click
        Choose3oKind()
    End Sub

    Private Sub btnChoose4oKind_Click(sender As Object, e As EventArgs) Handles btnChoose4oKind.Click
        Choose4oKind()
    End Sub

    Private Sub btnChooseFH_Click(sender As Object, e As EventArgs) Handles btnChooseFH.Click
        ChooseFH()
    End Sub

    Private Sub btnChooseSStraight_Click(sender As Object, e As EventArgs) Handles btnChooseSStraight.Click
        ChooseSStraight()
    End Sub

    Private Sub btnChooseLStraight_Click(sender As Object, e As EventArgs) Handles btnChooseLStraight.Click
        ChooseLStraight()
    End Sub

    Private Sub btnChooseChance_Click(sender As Object, e As EventArgs) Handles btnChooseChance.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Chance.Text = Convert.ToString(TotalofAllDice)
            p1ChooseLocks(12) = True
            LowerSectionTotals(0) += (TotalofAllDice)
        End If
        If currentplayer = 2 Then
            txtp2Chance.Text = Convert.ToString(TotalofAllDice)
            p2ChooseLocks(12) = True
            LowerSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 3 Then
            txtp3Chance.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(12) = True
            LowerSectionTotals(2) += (TotalofAllDice)
        End If
        If currentplayer = 4 Then
            txtp4Chance.Text = Convert.ToString(TotalofAllDice)
            p4ChooseLocks(12) = True
            LowerSectionTotals(3) += (TotalofAllDice)
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnChooseYahtzee_Click(sender As Object, e As EventArgs) Handles btnChooseYahtzee.Click
        ChooseYahtzee()
    End Sub

    Private Sub btnPlaceZeroAces_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroAces.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Aces.Text = "0"
            p1ChooseLocks(0) = True
        End If
        If currentplayer = 2 Then
            txtp2Aces.Text = "0"
            p2ChooseLocks(0) = True
        End If
        If currentplayer = 3 Then
            txtp3Aces.Text = "0"
            p3ChooseLocks(0) = True
        End If
        If currentplayer = 4 Then
            txtp4Aces.Text = "0"
            p4ChooseLocks(0) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZeroTwos_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroTwos.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Twos.Text = "0"
            p1ChooseLocks(1) = True
        End If
        If currentplayer = 2 Then
            txtp2Twos.Text = "0"
            p2ChooseLocks(1) = True
        End If
        If currentplayer = 3 Then
            txtp3Twos.Text = "0"
            p3ChooseLocks(1) = True
        End If
        If currentplayer = 4 Then
            txtp4Twos.Text = "0"
            p4ChooseLocks(1) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZeroThrees_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroThrees.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Threes.Text = "0"
            p1ChooseLocks(2) = True
        End If
        If currentplayer = 2 Then
            txtp2Threes.Text = "0"
            p2ChooseLocks(2) = True
        End If
        If currentplayer = 3 Then
            txtp3Threes.Text = "0"
            p3ChooseLocks(2) = True
        End If
        If currentplayer = 4 Then
            txtp4Threes.Text = "0"
            p4ChooseLocks(2) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZeroFours_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroFours.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Fours.Text = "0"
            p1ChooseLocks(3) = True
        End If
        If currentplayer = 2 Then
            txtp2Fours.Text = "0"
            p2ChooseLocks(3) = True
        End If
        If currentplayer = 3 Then
            txtp3Fours.Text = "0"
            p3ChooseLocks(3) = True
        End If
        If currentplayer = 4 Then
            txtp4Fours.Text = "0"
            p4ChooseLocks(3) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZeroFives_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroFives.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Fives.Text = "0"
            p1ChooseLocks(4) = True
        End If
        If currentplayer = 2 Then
            txtp2Fives.Text = "0"
            p2ChooseLocks(4) = True
        End If
        If currentplayer = 3 Then
            txtp3Fives.Text = "0"
            p3ChooseLocks(4) = True
        End If
        If currentplayer = 4 Then
            txtp4Fives.Text = "0"
            p4ChooseLocks(4) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZeroSixes_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroSixes.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Sixes.Text = "0"
            p1ChooseLocks(5) = True
        End If
        If currentplayer = 2 Then
            txtp2Sixes.Text = "0"
            p2ChooseLocks(5) = True
        End If
        If currentplayer = 3 Then
            txtp3Sixes.Text = "0"
            p3ChooseLocks(5) = True
        End If
        If currentplayer = 4 Then
            txtp4Sixes.Text = "0"
            p4ChooseLocks(5) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZero3oKind_Click(sender As Object, e As EventArgs) Handles btnPlaceZero3oKind.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1threeokind.Text = "0"
            p1ChooseLocks(6) = True
        End If
        If currentplayer = 2 Then
            txtp2threeokind.Text = "0"
            p2ChooseLocks(6) = True
        End If
        If currentplayer = 3 Then
            txtp3threeokind.Text = "0"
            p3ChooseLocks(6) = True
        End If
        If currentplayer = 4 Then
            txtp4threeokind.Text = "0"
            p4ChooseLocks(6) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZero4oKind_Click(sender As Object, e As EventArgs) Handles btnPlaceZero4oKind.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1fourokind.Text = "0"
            p1ChooseLocks(7) = True
        End If
        If currentplayer = 2 Then
            txtp2fourokind.Text = "0"
            p2ChooseLocks(7) = True
        End If
        If currentplayer = 3 Then
            txtp3fourokind.Text = "0"
            p3ChooseLocks(7) = True
        End If
        If currentplayer = 4 Then
            txtp4fourokind.Text = "0"
            p4ChooseLocks(7) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZeroFH_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroFH.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1FullHouse.Text = "0"
            p1ChooseLocks(8) = True
        End If
        If currentplayer = 2 Then
            txtp2FullHouse.Text = "0"
            p2ChooseLocks(8) = True
        End If
        If currentplayer = 3 Then
            txtp3FullHouse.Text = "0"
            p3ChooseLocks(8) = True
        End If
        If currentplayer = 4 Then
            txtp4FullHouse.Text = "0"
            p4ChooseLocks(8) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZeroSStraight_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroSStraight.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1SmallStraight.Text = "0"
            p1ChooseLocks(9) = True
        End If
        If currentplayer = 2 Then
            txtp2SmallStraight.Text = "0"
            p2ChooseLocks(9) = True
        End If
        If currentplayer = 3 Then
            txtp3SmallStraight.Text = "0"
            p3ChooseLocks(9) = True
        End If
        If currentplayer = 4 Then
            txtp4SmallStraight.Text = "0"
            p4ChooseLocks(9) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZeroLStraight_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroLStraight.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1LargeStraight.Text = "0"
            p1ChooseLocks(10) = True
        End If
        If currentplayer = 2 Then
            txtp2LargeStraight.Text = "0"
            p2ChooseLocks(10) = True
        End If
        If currentplayer = 3 Then
            txtp3LargeStraight.Text = "0"
            p3ChooseLocks(10) = True
        End If
        If currentplayer = 4 Then
            txtp4LargeStraight.Text = "0"
            p4ChooseLocks(10) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnPlaceZeroYahtzee_Click(sender As Object, e As EventArgs) Handles btnPlaceZeroYahtzee.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Yahtzee.Text = "0"
            p1ChooseLocks(11) = True
        End If
        If currentplayer = 2 Then
            txtp2Yahtzee.Text = "0"
            p2ChooseLocks(11) = True
        End If
        If currentplayer = 3 Then
            txtp3Yahtzee.Text = "0"
            p3ChooseLocks(11) = True
        End If
        If currentplayer = 4 Then
            txtp4Yahtzee.Text = "0"
            p4ChooseLocks(11) = True
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub


    Private Sub btnAddYTotalAces_Click(sender As Object, e As EventArgs) Handles btnAddYTotalAces.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Aces.Text = Convert.ToString(TotalofAllDice)
            p1ChooseLocks(0) = True
            UpperSectionTotals(0) += (TotalofAllDice)
        End If
        If currentplayer = 2 Then
            txtp2Aces.Text = Convert.ToString(TotalofAllDice)
            p2ChooseLocks(0) = True
            UpperSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 3 Then
            txtp3Aces.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(0) = True
            UpperSectionTotals(2) += (TotalofAllDice)
        End If
        If currentplayer = 4 Then
            txtp4Aces.Text = Convert.ToString(TotalofAllDice)
            p4ChooseLocks(0) = True
            UpperSectionTotals(3) += (TotalofAllDice)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnAddYTotalTwos_Click(sender As Object, e As EventArgs) Handles btnAddYTotalTwos.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Twos.Text = Convert.ToString(TotalofAllDice)
            p1ChooseLocks(1) = True
            UpperSectionTotals(0) += (TotalofAllDice)
        End If
        If currentplayer = 2 Then
            txtp2Twos.Text = Convert.ToString(TotalofAllDice)
            p2ChooseLocks(1) = True
            UpperSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 3 Then
            txtp3Twos.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(1) = True
            UpperSectionTotals(2) += (TotalofAllDice)
        End If
        If currentplayer = 4 Then
            txtp4Twos.Text = Convert.ToString(TotalofAllDice)
            p4ChooseLocks(1) = True
            UpperSectionTotals(3) += (TotalofAllDice)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnAddYTotalThrees_Click(sender As Object, e As EventArgs) Handles btnAddYTotalThrees.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Threes.Text = Convert.ToString(TotalofAllDice)
            p1ChooseLocks(2) = True
            UpperSectionTotals(0) += (TotalofAllDice)
        End If
        If currentplayer = 2 Then
            txtp2Threes.Text = Convert.ToString(TotalofAllDice)
            p2ChooseLocks(2) = True
            UpperSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 3 Then
            txtp3Threes.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(2) = True
            UpperSectionTotals(2) += (TotalofAllDice)
        End If
        If currentplayer = 4 Then
            txtp4Threes.Text = Convert.ToString(TotalofAllDice)
            p4ChooseLocks(2) = True
            UpperSectionTotals(3) += (TotalofAllDice)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnAddYTotalFours_Click(sender As Object, e As EventArgs) Handles btnAddYTotalFours.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Fours.Text = Convert.ToString(TotalofAllDice)
            p1ChooseLocks(3) = True
            UpperSectionTotals(0) += (TotalofAllDice)
        End If
        If currentplayer = 2 Then
            txtp2Fours.Text = Convert.ToString(TotalofAllDice)
            p2ChooseLocks(3) = True
            UpperSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 3 Then
            txtp3Fours.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(3) = True
            UpperSectionTotals(2) += (TotalofAllDice)
        End If
        If currentplayer = 4 Then
            txtp4Fours.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(3) = True
            UpperSectionTotals(3) += (TotalofAllDice)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnAddYTotalFives_Click(sender As Object, e As EventArgs) Handles btnAddYTotalFives.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Fives.Text = Convert.ToString(TotalofAllDice)
            p1ChooseLocks(4) = True
            UpperSectionTotals(0) += (TotalofAllDice)
        End If
        If currentplayer = 2 Then
            txtp2Fives.Text = Convert.ToString(TotalofAllDice)
            p2ChooseLocks(4) = True
            UpperSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 3 Then
            txtp3Fives.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(4) = True
            UpperSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 4 Then
            txtp4Fives.Text = Convert.ToString(TotalofAllDice)
            p4ChooseLocks(4) = True
            UpperSectionTotals(1) += (TotalofAllDice)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnAddYTotalSixes_Click(sender As Object, e As EventArgs) Handles btnAddYTotalSixes.Click
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Sixes.Text = Convert.ToString(TotalofAllDice)
            p1ChooseLocks(5) = True
            UpperSectionTotals(0) += (TotalofAllDice)
        End If
        If currentplayer = 2 Then
            txtp2Sixes.Text = Convert.ToString(TotalofAllDice)
            p2ChooseLocks(5) = True
            UpperSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 3 Then
            txtp3Sixes.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(5) = True
            UpperSectionTotals(2) += (TotalofAllDice)
        End If
        If currentplayer = 4 Then
            txtp4Sixes.Text = Convert.ToString(TotalofAllDice)
            p4ChooseLocks(5) = True
            UpperSectionTotals(3) += (TotalofAllDice)
        End If
        ShowSectionTotals()
        SixtyThreeCheck()
        btnNextPlayer.Visible = True
    End Sub

    Private Sub btnAddYTotal3oKind_Click(sender As Object, e As EventArgs) Handles btnAddYTotal3oKind.Click
        Choose3oKind()
    End Sub

    Private Sub btnAddYTotal4oKind_Click(sender As Object, e As EventArgs) Handles btnAddYTotal4oKind.Click
        Choose4oKind()
    End Sub

    Private Sub btnAddYTotalFH_Click(sender As Object, e As EventArgs) Handles btnAddYTotalFH.Click
        ChooseFH()
    End Sub

    Private Sub btnAddYTotalSStraight_Click(sender As Object, e As EventArgs) Handles btnAddYTotalSStraight.Click
        ChooseSStraight()
    End Sub

    Private Sub btnAddYTotalLStraight_Click(sender As Object, e As EventArgs) Handles btnAddYTotalLStraight.Click
        ChooseLStraight()
    End Sub

    Public Sub Choose3oKind()
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1threeokind.Text = Convert.ToString(TotalofAllDice)
            p1ChooseLocks(6) = True
            LowerSectionTotals(0) += (TotalofAllDice)
        End If
        If currentplayer = 2 Then
            txtp2threeokind.Text = Convert.ToString(TotalofAllDice)
            p2ChooseLocks(6) = True
            LowerSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 3 Then
            txtp3threeokind.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(6) = True
            LowerSectionTotals(2) += (TotalofAllDice)
        End If
        If currentplayer = 4 Then
            txtp4threeokind.Text = Convert.ToString(TotalofAllDice)
            p4ChooseLocks(6) = True
            LowerSectionTotals(3) += (TotalofAllDice)
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Public Sub Choose4oKind()
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1fourokind.Text = Convert.ToString(TotalofAllDice)
            p1ChooseLocks(7) = True
            LowerSectionTotals(0) += (TotalofAllDice)
        End If
        If currentplayer = 2 Then
            txtp2fourokind.Text = Convert.ToString(TotalofAllDice)
            p2ChooseLocks(7) = True
            LowerSectionTotals(1) += (TotalofAllDice)
        End If
        If currentplayer = 3 Then
            txtp3fourokind.Text = Convert.ToString(TotalofAllDice)
            p3ChooseLocks(7) = True
            LowerSectionTotals(2) += (TotalofAllDice)
        End If
        If currentplayer = 4 Then
            txtp4fourokind.Text = Convert.ToString(TotalofAllDice)
            p4ChooseLocks(7) = True
            LowerSectionTotals(3) += (TotalofAllDice)
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Public Sub ChooseFH()
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1FullHouse.Text = "25"
            p1ChooseLocks(8) = True
            LowerSectionTotals(0) += 25
        End If
        If currentplayer = 2 Then
            txtp2FullHouse.Text = "25"
            p2ChooseLocks(8) = True
            LowerSectionTotals(1) += 25
        End If
        If currentplayer = 3 Then
            txtp3FullHouse.Text = "25"
            p3ChooseLocks(8) = True
            LowerSectionTotals(2) += 25
        End If
        If currentplayer = 4 Then
            txtp4FullHouse.Text = "25"
            p4ChooseLocks(8) = True
            LowerSectionTotals(3) += 25
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Public Sub ChooseSStraight()
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1SmallStraight.Text = "30"
            p1ChooseLocks(9) = True
            LowerSectionTotals(0) += 30
        End If
        If currentplayer = 2 Then
            txtp2SmallStraight.Text = "30"
            p2ChooseLocks(9) = True
            LowerSectionTotals(1) += 30
        End If
        If currentplayer = 3 Then
            txtp3SmallStraight.Text = "30"
            p3ChooseLocks(9) = True
            LowerSectionTotals(2) += 30
        End If
        If currentplayer = 4 Then
            txtp4SmallStraight.Text = "30"
            p4ChooseLocks(9) = True
            LowerSectionTotals(3) += 30
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Public Sub ChooseLStraight()
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1LargeStraight.Text = "40"
            p1ChooseLocks(10) = True
            LowerSectionTotals(0) += 40
        End If
        If currentplayer = 2 Then
            txtp2LargeStraight.Text = "40"
            p2ChooseLocks(10) = True
            LowerSectionTotals(1) += 40
        End If
        If currentplayer = 3 Then
            txtp3LargeStraight.Text = "40"
            p3ChooseLocks(10) = True
            LowerSectionTotals(2) += 40
        End If
        If currentplayer = 4 Then
            txtp4LargeStraight.Text = "40"
            p4ChooseLocks(10) = True
            LowerSectionTotals(3) += 40
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub

    Public Sub ChooseYahtzee()
        DeactivateAllChoices()
        If currentplayer = 1 Then
            txtp1Yahtzee.Text = "50"
            p1ChooseLocks(11) = True
            LowerSectionTotals(0) += 50
        End If
        If currentplayer = 2 Then
            txtp2Yahtzee.Text = "50"
            p2ChooseLocks(11) = True
            LowerSectionTotals(1) += 50
        End If
        If currentplayer = 3 Then
            txtp3Yahtzee.Text = "50"
            p3ChooseLocks(11) = True
            LowerSectionTotals(2) += 50
        End If
        If currentplayer = 4 Then
            txtp4Yahtzee.Text = "50"
            p4ChooseLocks(11) = True
            LowerSectionTotals(3) += 50
        End If
        ShowSectionTotals()
        btnNextPlayer.Visible = True
    End Sub


    Public Sub ShowWinner(PlayerScores() As Integer)
        If PlayerScores(0) > PlayerScores(1) And PlayerScores(0) > PlayerScores(2) And PlayerScores(0) > PlayerScores(3) Then
            lblWinner.Text = "Player 1 Wins!"
            GameHighScore = PlayerScores(0)
        ElseIf PlayerScores(1) > PlayerScores(0) And PlayerScores(1) > PlayerScores(2) And PlayerScores(1) > PlayerScores(3) Then
            lblWinner.Text = "Player 2 Wins!"
            GameHighScore = PlayerScores(1)
        ElseIf PlayerScores(2) > PlayerScores(0) And PlayerScores(2) > PlayerScores(1) And PlayerScores(2) > PlayerScores(3) Then
            lblWinner.Text = "Player 3 Wins!"
            GameHighScore = PlayerScores(2)
        ElseIf PlayerScores(3) > PlayerScores(0) And PlayerScores(3) > PlayerScores(1) And PlayerScores(3) > PlayerScores(2) Then
            lblWinner.Text = "Player 4 Wins!"
            GameHighScore = PlayerScores(3)
        Else
            Dim matchcount As Integer = 0
            For x = 0 To 3
                If matchcount >= 2 Then
                    GameHighScore = PlayerScores(x)
                    Exit For
                End If
                matchcount = 0
                For y = 0 To 3
                    If PlayerScores(x) = PlayerScores(y) Then
                        matchcount += 1
                    End If
                Next
            Next
            lblWinner.Text = "It's a Draw!"
        End If
    End Sub

End Class

