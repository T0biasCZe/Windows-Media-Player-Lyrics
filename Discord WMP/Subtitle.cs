using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using static Discord_WMP.Subtitle;

namespace Discord_WMP {
	public class Subtitle {
		public string SongName { get; set; }
		public List<Segment> Segments { get; set; }
		public string WholeText { get; set; }

		int oldCurrentSegmentIndex = -1;
		public string GetFormattedForRichTextBox(double currentTime) {
			StringBuilder formattedText = new StringBuilder();
			formattedText.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0 Arial;}}"); // RTF header with font table

			bool currentSubtitleFound = false;
			int currentSegmentIndex = -1;
			for(int i = 0; i < Segments.Count; i++) {
				var segment = Segments[i];
				if(segment.Start <= currentTime && segment.End >= currentTime) {
					currentSegmentIndex = i;
					break;
				}
			}
			if(currentSegmentIndex == -1) currentSegmentIndex = oldCurrentSegmentIndex;
			oldCurrentSegmentIndex = currentSegmentIndex;

			int start = Math.Max(0, currentSegmentIndex - 1);

			for(int i = start; i < Segments.Count; i++) {
				var segment = Segments[i];

				if((segment.Start <= currentTime && segment.End >= currentTime) || i == currentSegmentIndex) {
					// Current subtitle
					formattedText.Append(@"\fs30\b "); // 15pt font, bold
					formattedText.Append(EscapeRtf(segment.Text));
					formattedText.Append(@"\b0\fs20\par "); // Reset to 10pt font
					currentSubtitleFound = true;
				}
				else if(currentSubtitleFound) {
					// Future subtitles
					formattedText.Append(@"\fs20 "); // 10pt font
					formattedText.Append(EscapeRtf(segment.Text));
					formattedText.Append(@"\par ");
				}
				else {
					// Previous subtitles
					formattedText.Append(@"\fs18 "); // 9pt font
					formattedText.Append(EscapeRtf(segment.Text));
					formattedText.Append(@"\par ");
				}
			}

			formattedText.Append("}"); // Closing the RTF group
			//Console.WriteLine(formattedText.ToString());
			return formattedText.ToString();
		}

		// Helper method to escape special RTF characters
		private string EscapeRtf(string text) {
			if(string.IsNullOrEmpty(text)) return string.Empty;
			return text.Replace(@"\", @"\\").Replace("{", @"\{").Replace("}", @"\}");
		}

		public static Subtitle LoadFromJson(string json) {
			CultureInfo culture = new CultureInfo("en-US");
			culture.NumberFormat.NumberDecimalSeparator = ".";

			var settings = new JsonSerializerSettings();
			settings.MissingMemberHandling = MissingMemberHandling.Ignore;
			settings.Culture = culture;

			return JsonConvert.DeserializeObject<Subtitle>(json, settings);
		}
	}

	public class Segment {
		public string Text { get; set; }
		public Double Start { get; set; }
		public Double End { get; set; }
	}
}