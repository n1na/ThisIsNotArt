using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ThisIsNotArt.Classes;
using ThisIsNotArt.Model;
using ThisIsNotArt.Resources;

namespace ThisIsNotArt.ViewModel
{
	public class MainViewModel : BaseViewModel
	{
		#region Fields
		private ObservableCollection<BoxData> _boxes = new ObservableCollection<BoxData>();
		private ICommand _exitCommand;
		private bool _isUiVisible = false;
		private MediaPlayer _mediaPlayer = new MediaPlayer();
		private Random _random = new Random();
		private ICommand _openUrlCommand;
		private ICommand _showUiCommand;
		#endregion Fields

		#region Constructor
		/// <summary>
		/// Creates instance of the Main ViewModel
		/// </summary>
		public MainViewModel()
		{
			PlayMusic();
		}
		#endregion Constructor

		#region Properties
		/// <summary>
		/// List containing the displayed boxes
		/// </summary>
		public ObservableCollection<BoxData> Boxes
		{
			get { return _boxes; }
			set { _boxes = value; Notify(() => Boxes); }
		}

		/// <summary>
		/// Command triggered when Esc is pressed
		/// </summary>
		public ICommand ExitCommand
		{
			get
			{
				return _exitCommand ?? (_exitCommand = new CommandHandler((param) => {
					App.Current.Shutdown();
				}, () => true));
			}
		}

		/// <summary>
		/// Specifies whether the title or the boxes are visible (false - title; true - boxes)
		/// </summary>
		public bool IsUiVisible
		{
			get { return _isUiVisible; }
			set { _isUiVisible = value; Notify(() => IsUiVisible); }
		}

		/// <summary>
		/// Command triggered when a box is clicked
		/// </summary>
		public ICommand OpenUrlCommand
		{
			get
			{
				return _openUrlCommand ?? (_openUrlCommand = new CommandHandler((param) => {
					OpenUrl((int)param);
				}, () => true));
			}
		}

		/// <summary>
		/// Command triggered when title is clicked
		/// </summary>
		public ICommand ShowUiCommand
		{
			get
			{
				return _showUiCommand ?? (_showUiCommand = new CommandHandler((param) => {
					LoadBoxes();
					IsUiVisible = true;
				}, () => true));
			}
		}
		#endregion Properties

		#region Functions
		/// <summary>
		/// Generates random top and left coordinate for a box
		/// </summary>
		/// <returns>Coordinate value</returns>
		public int CalculatePosition(int workArea, int size)
		{
			int maximum = workArea - (size + Constants.MarginSize);
			return _random.Next(Constants.MarginSize, maximum);
		}

		/// <summary>
		/// Populates a list of boxes and sets their size and position
		/// </summary>
		private void LoadBoxes()
		{
			// get number of texts to display
			var count = typeof(Strings).GetProperties().Where(p => p.Name.StartsWith("Text")).Count();

			for (int i = 1; i < count + 1; i++)
			{
				// use reflection to assign resource values to Boxes so the code isn't repeated for each value
				string counter = i < 9 ? "0" + i : i.ToString();
				var textPropertyName = typeof(Strings).GetProperties().Where(p => p.Name == string.Format("Text_{0}", counter)).First();
				var urlPropertyName = typeof(Strings).GetProperties().Where(p => p.Name == string.Format("Url_{0}", counter)).First();

				BoxData boxData = new BoxData();
				boxData.Id = i;
				boxData.Text = (string)textPropertyName.GetValue(null, null);
				boxData.Url = (string)urlPropertyName.GetValue(null, null);

				// randomize size and position
				boxData.Size = _random.Next(Constants.BoxMinSize, Constants.BoxMaxSize);

				// calculate box position on screen
				int leftValue = CalculatePosition((int)SystemParameters.WorkArea.Right, Constants.BoxMaxWidthExpanded);
				int topValue = CalculatePosition((int)SystemParameters.WorkArea.Bottom, Constants.BoxMaxHeightExpanded);

				// if there are more than 0 boxes - check if the current box overlaps an existing one
				if(Boxes.Count > 0)
				{
					while (Boxes.Any(b=> b.CheckDistance(leftValue, topValue, boxData.Size)))
					{
						leftValue = CalculatePosition((int)SystemParameters.WorkArea.Right, Constants.BoxMaxWidthExpanded);
						topValue = CalculatePosition((int)SystemParameters.WorkArea.Bottom, Constants.BoxMaxHeightExpanded);
					}
				}

				boxData.Left = leftValue;
				boxData.Top = topValue;

				Boxes.Add(boxData);
			}
		}

		/// <summary>
		/// Opens the URL when a box is clicked
		/// </summary>
		private void OpenUrl(int boxId)
		{
			var clickedBox = Boxes.Where(b => b.Id == boxId).FirstOrDefault();

			var psi = new System.Diagnostics.ProcessStartInfo();
			psi.UseShellExecute = true;
			psi.FileName = clickedBox?.Url;
			System.Diagnostics.Process.Start(psi);
		}

		/// <summary>
		/// Starts the media player
		/// </summary>
		private void PlayMusic()
		{
			_mediaPlayer.Open(new Uri("Assets\\Music.mp3", UriKind.Relative));
			_mediaPlayer.Play();
		}
		#endregion Functions
	}
}
