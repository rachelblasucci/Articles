module MainApp

open System
open System.Windows
open System.Windows.Controls
open FSharpx

type MainWindow = XAML<"MainWindow.xaml">

let window = MainWindow()
let _viewModel = new FormatTextViewModel.FormatTextViewModel()

let loadWindow() =
    _viewModel.PropertyChanged 
        |> Event.add (fun _ -> window.FormattedTextBox.Document <- _viewModel.GetFormattedText)

    window.UnformattedTextBox.KeyUp.Add(fun _ -> _viewModel.Text <- window.UnformattedTextBox.Text)

    window.Root.DataContext <- _viewModel
    window.Root

[<STAThread>]
(new Application()).Run(loadWindow()) |> ignore
