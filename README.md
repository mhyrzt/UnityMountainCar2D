# Mountain Car 2D Unity

![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![Python](https://img.shields.io/badge/python-3670A0?style=for-the-badge&logo=python&logoColor=ffdd54)

Mountain Car problem implemented with the help of Unity and [Unity ML-Agents Toolkit](https://github.com/Unity-Technologies/ml-agents), which give you high customizability over the environment, you can change the following properties:

- position of goal by changing Ground sprite
- Add an offset to the current height of the goal to increase its height.
- Change the gravity of the environment
- Change the mass of the car.
- Change the force of the car.

```python
from mcu2d import MountainCarUnity

env = MountainCarUnity(f"{PATH_TO_BUILD_FOLDER}/MountainCar")
state = env.reset()
done = False
while not done:
    action = env.action_space.sample()
    state, reward, done, _ = env.step(action)

```
[video.mp4](https://github.com/mhyrzt/UnityMountainCar2D/blob/main/video.mp4?raw=true)
## Notes

- make sure you check the __"Development Build"__ in the build setting.
- for faster training, you can increase `time_scale` and disable graphics.

```python
env = MountainCarUnity("MountainCar", no_graphics=True, time_scale=100_000)
```
