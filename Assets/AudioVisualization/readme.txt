# Audio Visualization

Simple blocks visualizing audio by increasing their y-scale and material color emission.
By default there are eight blocks listening to frequency bands of an audio source.
There is an audio peer instance calculating the necessary samples and bands to be used by many game objects.

by BadToxic (http://badtoxic.de)

— Content —

- *AudioPeer* prefab & C# script for fetching and calculating data from an AudioSource
- *AudioSpectrum* prefab & C# script creating 8 audio bumper
- *AudioBumper* prefab & C# script scaling and coloring to data by the AudioPeer
- *AudioVisualizationScene* for demonstration

— Requirements —

Audio file (eg. mp3) to be visualized.

— Usage —

Open the given scene and add an audio file to the existing AudioSource and start the game.
You can use the AudioPeer's _GetFrequencyBand(int freqIndex)_ method to get a frequency band consisting of several audio samples
or the _GetSample(int sampleIndex)_ method for a single sample. All band values are normalized floats between 0 and 1.
By default there are 512 samples that get devided into 8 frequency bands using their averages.
The values get buffered for a smoother experience.
The code is easy to adjust to be able to change the number of samples or bands.

— Support —

Need help? Join my discord server: https://discord.gg/8QMCm2d

— Follow me —

Support me with likes and follows/subscriptions:
[Instagram](https://www.instagram.com/xybadtoxic)
[Twitter](https://twitter.com/BadToxic)
[YouTube](https://www.youtube.com/user/BadToxic)
[Facebook](https://www.facebook.com/XyBadToxic)