

# Une intelligence artificielle qui apprend dans unity

Ce dépôt contient les ressources et le projet unity developpé dans la vidéo "Une intelligence artificielle prend le contrôle de mon jeu (Tutoriel complet Unity ml-agent)".

## Requis

Réalisé avec Unity 2019.4.13f1 et python 3.6.5

Installation ml-agent :

* Dans le répertoire de votre choix, commencez par créer un environnement virtuel

``python -m venv myvenv``

* Activez votre environnement virtuel  :

``myvenv\Scripts\activate.bat``   (Windows)

ou

``source mypython/bin/activate``   (Linux)

* Puis installez Pytorch :

``pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html``

* Et enfin installez ml-agent release 15 : 

``python -m pip install mlagents==0.25.0``

## Contenu du dépôt

* Projet unity "Target Coin"

* Config pour l'entrainement de ml-agent (modèle entrainé `My Behavior.onnx` disponible dans les assets du projet unity)

