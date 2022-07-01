import numpy as np
import mlagents
from mlagents_envs.base_env import ActionTuple, BehaviorSpec
from mlagents_envs.environment import UnityEnvironment
from mlagents_envs.side_channel.engine_configuration_channel import EngineConfigurationChannel


class ObservationSpace:
    def __init__(self, low, high) -> None:
        self.low = np.array(low)
        self.high = np.array(high)
        self.shape = self.high.shape


class ActionSpace:
    def __init__(self, spec: BehaviorSpec) -> None:
        self.spec = spec
        self.n = 3

    def sample(self) -> int:
        return self.spec.action_spec.random_action(1).discrete[0][0]


class MountainCarUnity:
    def __init__(self, file_name, no_graphics=False, time_scale=1_000) -> None:
        self.observation_space = ObservationSpace((-20, -20), (20, 20))

        self.channel = EngineConfigurationChannel()
        self.env = UnityEnvironment(
            file_name=file_name,
            seed=1,
            side_channels=[self.channel],
            no_graphics=no_graphics,
        )
        self.channel.set_configuration_parameters(time_scale=time_scale)
        self.env.reset()
        self.behavior_name = list(self.env.behavior_specs)[0]
        self.spec = self.env.behavior_specs[self.behavior_name]
        self.env.reset()

        self.decision_steps, self.terminal_step = self.env.get_steps(
            self.behavior_name)

        self.tracked_agent = self.decision_steps.agent_id[0]

        self.action_space = ActionSpace(self.spec)
        self.max_step = 200
        self.step_counter = 0
        self.goal_position = 14.10568

    def reset(self):
        self.env.reset()
        self.step_counter = 0
        return self.get_observation()

    def close(self):
        self.env.close()

    def render(self):
        return

    def step(self, action):
        self.env.set_actions(self.behavior_name, self._get_action(action))
        self.env.step()
        self.decision_steps, self.terminal_step = self.env.get_steps(
            self.behavior_name)
        self.step_counter += 1

        return (self.get_observation(), *self.get_reward_done(), {"info": None})

    def get_reward_done(self):
        return (-1, (self.tracked_agent in self.terminal_step) or (self.step_counter == self.max_step))

    def get_observation(self):
        if self.tracked_agent in self.decision_steps:
            return self.decision_steps[self.tracked_agent].obs[0]

        return self.terminal_step[self.tracked_agent].obs[0]

    def _get_action(self, action: int) -> ActionTuple:
        return ActionTuple(discrete=np.array([[action]]))
