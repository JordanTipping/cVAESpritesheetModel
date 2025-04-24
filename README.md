# AI-Spritesheets-Demo
 

This repository contains the code for the demo, of the result of experimentation to create
AI generated spritesheets using a Variational Autoencoder. 

It is a very simple showcase of the sprites.

The repository also contains a zipped .exe(buildfiles.zip), to save downloading
the code and running in the editor, one can download the zip and run the demo. 

# Training model 

There is a python notebook file included (CVAEDEPLOYMENT.ipynb)

Because of hardware constraints, the model was trained using a google colab GPU, so the file is provided. 

1. Open Colab

2. File - > Upload Notebook. 

3. (CVAEDEPLOYMENT.ipynb) from this repo.


The dataset is in this repo under (Dataset.zip)

Dataset must be saved in content/spritesheets. Thats the root of the colab notebooks file directory, in a folder called spritesheets. 

Step through the code cells. 

Train... Look for num_epochs Variable

1000 Epochs works, but 5000 is reccommended. 

Generate...

Try prompts like:

Hooded or 
Blue Hooded

Helmet or
Yellow Helmet


And other combinations - related to the name of the files in the dataset (included), to see how the model interprets prompts.

The result is (usually), usable 4 directional spritesheets, only limited by the dataset that you give the model to learn from! 







