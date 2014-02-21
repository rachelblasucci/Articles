module MainApp

open System
open System.Windows
open System.Windows.Controls
open FSharpx
open FSharp.Control.Observable

type MainWindow = XAML<"MainWindow.xaml">

let window = MainWindow()
let _viewModel = new FormatTextViewModel.FormatTextViewModel()

let loadWindow() =
    window.Root.DataContext <- _viewModel
    window.Root

let OriginalTextLoop() = 
    async {
        while true do 
            let! keyup = Async.AwaitObservable(window.UnformattedTextBox.KeyUp)
            _viewModel.UnformattedText <- window.UnformattedTextBox.Text
    }

let FormattedTextLoop() = 
    async {
        while true do 
            let! formattedTextChanged = Async.AwaitObservable(_viewModel.PropertyChanged)
            let! fd = _viewModel.GetFormattedText
            window.FormattedTextBox.Document <- fd
    }

[<STAThread>]
do
    Async.StartImmediate(OriginalTextLoop())
    Async.StartImmediate(FormattedTextLoop())
    (new Application()).Run(loadWindow()) |> ignore
