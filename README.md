# Windows Media Player Lyrics info           
Download: https://github.com/T0biasCZe/Windows-Media-Player-Discord-RPC/releases/tag/v2.2.5

Can display lyrics from:     
 * Standardized .lrc format    
 * json generated by faster whisper standalone program     
The lyrics file has to be next to the audio file with the same file name     

Album art should be loaded automatically, using thumbnails generated by Windows Media Player. In case wrong thumbnail is used, you can override it by making jpg file with the same file name next to the music file.       
In case music metadata is not loaded (For example, when playing ogg files on Windows 7), you can side load the metadata from .meta.csv file next to the music file. The csv should be formatted like this:      
Artist; Name of the Artist    
Album; Name of the Album    
Title; Name of the song    

Person who added the song to playlist can be shown by using .pick text file next to the music file      

![VID_20250117_200823cut mp4_snapshot_00 06 305](https://github.com/user-attachments/assets/338f367e-d105-4568-a33d-18d1ce5fae11)
