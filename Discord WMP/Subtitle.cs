﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
		public static Subtitle LoadFromLrc(string text) {
			var segments = new List<Segment>();
			var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach(var line in lines) {
				// Match [mm:ss.xx] or [mm:ss] at the start of the line
				var matches = System.Text.RegularExpressions.Regex.Matches(line, @"\[(\d{1,2}):(\d{2})(?:\.(\d{1,2}))?\]");
				if(matches.Count == 0) continue;

				// The lyric text is after the last timestamp
				int lastBracket = line.LastIndexOf(']');
				string lyric = lastBracket >= 0 && lastBracket < line.Length - 1 ? line.Substring(lastBracket + 1).Trim() : "";

				foreach(System.Text.RegularExpressions.Match match in matches) {
					int min = int.Parse(match.Groups[1].Value);
					int sec = int.Parse(match.Groups[2].Value);
					int ms = match.Groups[3].Success ? int.Parse(match.Groups[3].Value.PadRight(2, '0')) * 10 : 0;
					double start = new TimeSpan(0, 0, min, sec, ms).TotalSeconds;

					segments.Add(new Segment {
						Start = start,
						End = start + 5, // Default duration, will be fixed below
						Text = lyric
					});
				}
			}

			// Sort and set End times to the next Start (except last)
			segments = segments.OrderBy(s => s.Start).ToList();
			for(int i = 0; i < segments.Count - 1; i++) {
				segments[i].End = segments[i + 1].Start;
			}
			if(segments.Count > 0)
				segments[segments.Count - 1].End = segments[segments.Count - 1].Start + 5; // Last segment: 5s duration

			return new Subtitle {
				Segments = segments,
				WholeText = string.Join(" ", segments.Select(s => s.Text))
			};
		}
	}

	public class Segment {
		public string Text { get; set; }
		public Double Start { get; set; }
		public Double End { get; set; }
	}
}