import unittest
import testingtools
from testingtools import load_save
import krpc
import time

class TestControl(testingtools.TestCase):

    @classmethod
    def setUpClass(cls):
        load_save('control')
        cls.conn = krpc.connect()
        cls.control = cls.conn.space_center.active_vessel.control
        vessel = cls.conn.space_center.active_vessel
        cls.orbital_flight = vessel.flight(cls.conn.space_center.ReferenceFrame.orbital)

    def test_equality(self):
        self.assertEqual(self.conn.space_center.active_vessel.control, self.control)

    def test_special_action_groups(self):
        for name in ['sas', 'rcs', 'gear', 'lights', 'brakes', 'abort']:
            setattr(self.control, name, True)
            self.assertTrue(getattr(self.control, name))
            setattr(self.control, name, False)
            self.assertFalse(getattr(self.control, name))

    def test_numeric_action_groups(self):
        for i in [0,1,2,3,4,5,6,7,8,9]:
            self.control.set_action_group(i, False)
            self.assertFalse(self.control.get_action_group(i))
            self.control.set_action_group(i, True)
            self.assertTrue(self.control.get_action_group(i))
            self.control.toggle_action_group(i)
            self.assertFalse(self.control.get_action_group(i))
        self.assertRaises(krpc.client.RPCError, self.control.set_action_group, 11, False)
        self.assertRaises(krpc.client.RPCError, self.control.get_action_group, 11)
        self.assertRaises(krpc.client.RPCError, self.control.toggle_action_group, 11)

    def test_maneuver_node_editing(self):
        node = self.control.add_node(self.conn.space_center.ut + 60, 100, 0, 0)
        self.assertEquals(100, node.prograde)
        self.control.remove_nodes()

    def test_pitch_control(self):
        self.setUpClass()

        self.control.sas = False
        self.control.pitch = 1
        time.sleep(3)
        self.control.pitch = 0

        # Check flight is pitching in correct direction
        pitch = self.orbital_flight.pitch
        time.sleep(0.1)
        diff = self.orbital_flight.pitch - pitch
        self.assertGreater(diff, 0)

    def test_yaw_control(self):
        self.setUpClass()

        self.control.sas = False
        self.control.yaw = 1
        time.sleep(3)
        self.control.yaw = 0

        # Check flight is yawing in correct direction
        heading = self.orbital_flight.heading
        time.sleep(0.1)
        diff = self.orbital_flight.heading - heading
        self.assertGreater(diff, 0)

    def test_roll_control(self):
        self.setUpClass()

        pitch = self.orbital_flight.pitch
        heading = self.orbital_flight.heading

        self.control.sas = False
        self.control.roll = 0.1
        time.sleep(3)
        self.control.roll = 0

        self.assertClose(pitch, self.orbital_flight.pitch, error=1)
        self.assertClose(heading, self.orbital_flight.heading, error=1)

        # Check flight is rolling in correct direction
        roll = self.orbital_flight.roll
        time.sleep(0.1)
        diff = self.orbital_flight.roll - roll
        self.assertGreater(diff, 0)

    def test_staging_single(self):
        self.assertEqual(3, self.control.current_stage)
        time.sleep(0.5)
        self.control.activate_next_stage()
        self.assertEqual(2, self.control.current_stage)
        time.sleep(0.5)
        self.control.activate_next_stage()
        self.assertEqual(1, self.control.current_stage)
        time.sleep(0.5)
        self.control.activate_next_stage()
        self.assertEqual(0, self.control.current_stage)
        time.sleep(0.5)
        self.control.activate_next_stage()
        self.assertEqual(0, self.control.current_stage)

if __name__ == "__main__":
    unittest.main()
