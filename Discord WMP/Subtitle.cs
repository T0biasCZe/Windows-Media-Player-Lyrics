using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Discord_WMP {
	public class Subtitle {
		public string SongName { get; set; }
		public List<Segment> Segments { get; set; }
		public string WholeText { get; set; }

		public string GetFormattedForRichTextBox(TimeSpan currentTime) {
			StringBuilder formattedText = new StringBuilder();
			bool currentSubtitleFound = false;

			for(int i = 0; i < Segments.Count; i++) {
				var segment = Segments[i];
				if(segment.Start <= currentTime && segment.End >= currentTime) {
					// Current subtitle
					formattedText.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0 Arial;}}");
					formattedText.Append(@"\fs24\b "); // 12pt font, bold
					formattedText.Append(segment.Text);
					formattedText.Append(@"\b0\fs18\par "); // Reset to 9pt font
					currentSubtitleFound = true;
				}
				else if(currentSubtitleFound) {
					// Future subtitles
					formattedText.Append(@"\fs18 "); // 9pt font
					formattedText.Append(segment.Text);
					formattedText.Append(@"\par ");
				}
				else {
					// Previous subtitles
					formattedText.Append(@"\fs18 "); // 9pt font
					formattedText.Append(segment.Text);
					formattedText.Append(@"\par ");
				}
			}

			formattedText.Append(@"}");
			return formattedText.ToString();
		}

		public static Subtitle LoadFromJson(string json) {
			return JsonConvert.DeserializeObject<Subtitle>(json, new JsonSerializerSettings {
				MissingMemberHandling = MissingMemberHandling.Ignore
			});
		}
	}

	public class Segment {
		public string Text { get; set; }
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }
	}
}