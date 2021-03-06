#!/usr/bin/env python2

import unittest
import testingtools
from testingtools import load_save
import krpc
import math

class TestBody(testingtools.TestCase):

    @classmethod
    def setUpClass(cls):
        load_save('basic')
        cls.conn = krpc.connect()

    def test_equality(self):
        bodies = self.conn.space_center.bodies
        bodies2 = self.conn.space_center.bodies
        for key,body in bodies.items():
            self.assertEqual(bodies2[key], body)

    def test_kerbin(self):
        kerbin = self.conn.space_center.bodies['Kerbin']
        self.assertEqual('Kerbin', kerbin.name)
        self.assertClose(5.2915e22, kerbin.mass, error=0.0001e22)
        self.assertClose(3.5316e12, kerbin.gravitational_parameter, error=0.0001e12)
        self.assertClose(9.81, kerbin.surface_gravity)
        self.assertClose(21600, kerbin.rotational_period)
        self.assertClose(600000, kerbin.equatorial_radius)
        self.assertClose(8.4159e7, kerbin.sphere_of_influence, error=0.0001e7)
        self.assertClose(1.36e10, kerbin.orbit.apoapsis, error=0.0001e10)
        self.assertClose(1.36e10, kerbin.orbit.periapsis, error=0.0001e10)
        self.assertEqual(True, kerbin.has_atmosphere)
        self.assertClose(101325, kerbin.atmosphere_pressure)
        self.assertClose(1.0 * 1.2230948554874, kerbin.atmosphere_density)
        self.assertClose(5000, kerbin.atmosphere_scale_height)
        self.assertClose(70000, kerbin.atmosphere_max_altitude)
        self.assertClose(kerbin.atmosphere_pressure, kerbin.atmosphere_pressure_at(0))
        self.assertClose(kerbin.atmosphere_density, kerbin.atmosphere_density_at(0))
        self.assertClose(kerbin.atmosphere_pressure * math.exp(-1), kerbin.atmosphere_pressure_at(5000))
        self.assertClose(kerbin.atmosphere_density * math.exp(-1), kerbin.atmosphere_density_at(5000))
        self.assertClose(kerbin.atmosphere_pressure * math.exp(-2), kerbin.atmosphere_pressure_at(10000))
        self.assertClose(kerbin.atmosphere_density * math.exp(-2), kerbin.atmosphere_density_at(10000))
        self.assertClose(0, kerbin.atmosphere_pressure_at(float('inf')))
        self.assertClose(0, kerbin.atmosphere_density_at(float('inf')))
        self.assertClose(kerbin.atmosphere_pressure * math.exp(1), kerbin.atmosphere_pressure_at(-5000))
        self.assertClose(kerbin.atmosphere_density * math.exp(1), kerbin.atmosphere_density_at(-5000))
        self.assertClose(float('inf'), kerbin.atmosphere_pressure_at(-float('inf')))
        self.assertClose(float('inf'), kerbin.atmosphere_density_at(-float('inf')))

    def test_mun(self):
        mun = self.conn.space_center.bodies['Mun']
        self.assertEqual('Mun', mun.name)
        self.assertClose(9.76e20, mun.mass, error=0.0001e20)
        self.assertClose(6.5138e10, mun.gravitational_parameter, error=0.0001e10)
        self.assertClose(1.6285, mun.surface_gravity)
        self.assertClose(1.3898e5, mun.rotational_period, error=0.0001e5)
        self.assertClose(200000, mun.equatorial_radius)
        self.assertClose(2.4296e6, mun.sphere_of_influence, error=0.0001e6)
        self.assertClose(1.2e7, mun.orbit.apoapsis, error=0.0001e7)
        self.assertClose(1.2e7, mun.orbit.periapsis, error=0.0001e7)
        self.assertEqual(False, mun.has_atmosphere)
        self.assertClose(0, mun.atmosphere_pressure)
        self.assertClose(0, mun.atmosphere_density)
        self.assertClose(0, mun.atmosphere_scale_height)
        self.assertClose(0, mun.atmosphere_max_altitude)
        self.assertClose(0, mun.atmosphere_pressure_at(5000))
        self.assertClose(0, mun.atmosphere_density_at(5000))

    def test_minmus(self):
        minmus = self.conn.space_center.bodies['Minmus']
        self.assertEqual('Minmus', minmus.name)
        self.assertClose(4.7e7, minmus.orbit.apoapsis, error=0.0001e7)
        self.assertClose(4.7e7, minmus.orbit.periapsis, error=0.0001e7)
        self.assertClose(6, minmus.orbit.inclination)
        self.assertEqual(False, minmus.has_atmosphere)

    def test_sun(self):
        sun = self.conn.space_center.bodies['Sun']
        self.assertEqual('Sun', sun.name)
        self.assertClose(1.7566e28, sun.mass, error=0.0001e28)
        self.assertClose(1.1723e18, sun.gravitational_parameter, error=0.0001e18)
        self.assertClose(2.616e8, sun.equatorial_radius, error=0.0001e8)
        self.assertEqual(float('inf'), sun.sphere_of_influence)
        self.assertEqual(None, sun.orbit)
        self.assertEqual(False, sun.has_atmosphere)

    def test_duna(self):
        duna = self.conn.space_center.bodies['Duna']
        self.assertEqual(True, duna.has_atmosphere)
        self.assertClose(20265, duna.atmosphere_pressure)
        self.assertClose(0.2 * 1.2230948554874, duna.atmosphere_density)
        self.assertClose(3000, duna.atmosphere_scale_height)
        self.assertClose(50000, duna.atmosphere_max_altitude)
        self.assertClose(duna.atmosphere_pressure, duna.atmosphere_pressure_at(0))
        self.assertClose(duna.atmosphere_density, duna.atmosphere_density_at(0))
        self.assertClose(duna.atmosphere_pressure * math.exp(-1), duna.atmosphere_pressure_at(3000))
        self.assertClose(duna.atmosphere_density * math.exp(-1), duna.atmosphere_density_at(3000))
        self.assertClose(duna.atmosphere_pressure * math.exp(-2), duna.atmosphere_pressure_at(6000))
        self.assertClose(duna.atmosphere_density * math.exp(-2), duna.atmosphere_density_at(6000))

    def test_system(self):
        bodies = self.conn.space_center.bodies
        kerbin = bodies['Kerbin']
        mun = bodies['Mun']
        minmus = bodies['Minmus']
        sun = bodies['Sun']
        duna = bodies['Duna']
        ike = bodies['Ike']

        self.assertEqual(kerbin, mun.orbit.body)
        self.assertEqual(kerbin, minmus.orbit.body)
        self.assertEqual(sun, kerbin.orbit.body)
        self.assertEqual(sun, duna.orbit.body)

        self.assertNotEqual(sun, mun.orbit.body)
        self.assertNotEqual(sun, minmus.orbit.body)
        self.assertNotEqual(mun, kerbin.orbit.body)

        self.assertEqual(set([mun,minmus]), set(kerbin.satellites))
        self.assertEqual(set([ike]), set(duna.satellites))
        self.assertEqual(set(), set(mun.satellites))

if __name__ == "__main__":
    unittest.main()
