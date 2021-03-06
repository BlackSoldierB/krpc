import unittest
import testingtools
from testingtools import load_save
from mathtools import vector, norm, dot
import math
import krpc

class TestOrbit(testingtools.TestCase):

    def test_orbit_kerbin(self):
        load_save('orbit-kerbin')
        ksp = krpc.connect()
        vessel = ksp.space_center.active_vessel
        orbit = vessel.orbit
        # inc   0
        # e     0
        # sma   70000
        # lan   0
        # w     0
        # mEp   0
        # epoch 0
        # body  Kerbin
        self.assertEqual('Kerbin', orbit.body.name)
        self.assertClose(100000 + 600000, orbit.apoapsis, error=10)
        self.assertClose(100000 + 600000, orbit.periapsis, error=10)
        self.assertClose(100000,  orbit.apoapsis_altitude, error=10)
        self.assertClose(100000, orbit.periapsis_altitude, error=10)
        self.assertClose(100000 + 600000, orbit.semi_major_axis, error=10)
        self.assertClose(100000, orbit.semi_major_axis_altitude, error=10)
        self.assertClose(700000, orbit.radius, error=10)
        self.assertClose(2246.1, orbit.speed, error=1)
        self.assertClose(603.48, orbit.time_to_apoapsis, error=1)
        self.assertClose(1582.5, orbit.time_to_periapsis, error=1)
        self.assertTrue(math.isnan(orbit.time_to_soi_change))
        self.assertClose(0, orbit.eccentricity, error=0.1)
        self.assertClose(0, orbit.inclination, error=0.1)
        self.assertClose(0, orbit.longitude_of_ascending_node, error=0.1)
        self.assertClose(0, orbit.argument_of_periapsis, error=0.1)
        self.assertClose(0, orbit.mean_anomaly_at_epoch, error=0.1)
        self.assertEqual(None, orbit.next_orbit)

    def test_orbit_bop(self):
        load_save('orbit-bop')
        ksp = krpc.connect()
        vessel = ksp.space_center.active_vessel
        orbit = vessel.orbit
        # inc   27
        # e     0.18
        # sma   320000
        # lan   38
        # w     241
        # mEp   2.3
        # epoch 0
        # body  Kerbin
        self.assertEqual('Bop', orbit.body.name)
        self.assertClose(377600,  orbit.apoapsis, error=10)
        self.assertClose(262400 , orbit.periapsis, error=10)
        self.assertClose(377600 - 65000,  orbit.apoapsis_altitude, error=10)
        self.assertClose(262400 - 65000, orbit.periapsis_altitude, error=10)
        self.assertClose(0.5 * (377600 + 262400), orbit.semi_major_axis, error=10)
        self.assertClose(0.5 * (377600 - 65000 + 262400 - 65000), orbit.semi_major_axis_altitude, error=10)
        self.assertClose(366329, orbit.radius, error=10)
        self.assertClose(76, orbit.speed, error=1)
        self.assertClose(2698.33, orbit.time_to_apoapsis, error=1)
        self.assertClose(14102.17, orbit.time_to_periapsis, error=1)
        self.assertTrue(math.isnan(orbit.time_to_soi_change))
        self.assertClose(0.18, orbit.eccentricity, error=0.1)
        self.assertClose(27, orbit.inclination, error=0.1)
        self.assertClose(38, orbit.longitude_of_ascending_node, error=0.1)
        self.assertClose(241, orbit.argument_of_periapsis, error=0.1)
        self.assertClose(2.3, orbit.mean_anomaly_at_epoch, error=0.1)
        self.assertEqual(None, orbit.next_orbit)

    def test_orbit_mun_escape_soi(self):
        load_save('orbit-mun-escape-soi')
        ksp = krpc.connect()
        vessel = ksp.space_center.active_vessel
        orbit = vessel.orbit
        # inc   0
        # e     0.52
        # sma   1800000
        # lan   13
        # w     67
        # mEp   6.25
        # epoch 0
        # body  Mun
        self.assertEqual('Mun', orbit.body.name)
        self.assertClose(2736000, orbit.apoapsis, error=10)
        self.assertClose(864000, orbit.periapsis, error=10)
        self.assertClose(2736000 - 200000, orbit.apoapsis_altitude, error=10)
        self.assertClose(864000 - 200000, orbit.periapsis_altitude, error=10)
        self.assertClose(0.5 * (2736000 + 864000), orbit.semi_major_axis, error=10)
        self.assertClose(0.5 * (2736000 - 200000 + 864000 - 200000), orbit.semi_major_axis_altitude, error=10)
        self.assertClose(865546, orbit.radius, error=10)
        self.assertClose(338.1, orbit.speed, error=1)
        self.assertClose(29987.92, orbit.time_to_apoapsis, error=1)
        self.assertClose(261.65, orbit.time_to_periapsis, error=1)
        self.assertClose(18464, orbit.time_to_soi_change,error=5)
        self.assertClose(0.52, orbit.eccentricity, error=0.1)
        self.assertClose(0, orbit.inclination, error=0.1)
        # TODO: fix this
        #self.assertClose(13, orbit.longitude_of_ascending_node, error=0.1)
        #self.assertClose(67, orbit.argument_of_periapsis, error=0.1)
        #self.assertClose(6.2, orbit.mean_anomaly_at_epoch, error=0.1)
        self.assertTrue(orbit.next_orbit is not None)

        orbit = orbit.next_orbit
        self.assertEqual('Kerbin', orbit.body.name)
        self.assertClose(25224000, orbit.apoapsis, error=1000)
        self.assertClose(12428000, orbit.periapsis, error=1000)

    """
    def test_orbit_minmus_parabolic(self):
        load_save('orbit-minmus-parabolic')
        ksp = krpc.connect()
        vessel = ksp.space_center.active_vessel
        orbit = vessel.orbit
        self.assertEqual('Minmus', orbit.body.name)
        self.assertClose(-175327.32795440647, orbit.apoapsis)
        self.assertClose(87187.64537168786, orbit.periapsis)
        self.assertClose(-235327.32795440647, orbit.apoapsis_altitude)
        self.assertClose(27187.64537168786, orbit.periapsis_altitude)
        self.assertClose(0, orbit.time_to_apoapsis, error=0.5)
        self.assertClose(1024.43, orbit.time_to_periapsis, error=0.5)
        self.assertClose(2.97839708101655, orbit.eccentricity)
        self.assertClose(168.280967855609, orbit.inclination)
        self.assertClose(181.171756205933, orbit.longitude_of_ascending_node)
        self.assertClose(165.50774557981, orbit.argument_of_periapsis)
        self.assertClose(-4.65482114687744, orbit.mean_anomaly_at_epoch)
        self.assertClose(0, orbit.radius)
        self.assertClose(0, orbit.speed)
    """

if __name__ == "__main__":
    unittest.main()
