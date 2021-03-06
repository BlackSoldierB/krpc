import krpc
import time
import unittest
import os
import shutil
import itertools

def load_save(name):
    save_dir = os.getenv('KSP_DIR') + '/saves/test'
    if not os.path.exists(save_dir):
        os.makedirs(save_dir)
    shutil.copy(os.path.dirname(os.path.realpath(__file__)) + '/fixtures/' + name + '.sfs', save_dir + '/' + name + '.sfs')
    # Connect and issue load save RPC
    ksp = krpc.connect(name='testingtools.load_save')
    ksp.testing_tools.load_save('test', name)
    time.sleep(1)
    # Wait until server comes back up
    while True:
        try:
            ksp = krpc.connect(name='testingtools.load_save')
            break
        except:
            time.sleep(0.2)
            pass
    # Wait until the vessel is loaded properly
    time.sleep(0.2)

class TestCase(unittest.TestCase):

    def assertBetween(self, min_value, max_value, value):
        self.assertLessEqual(min_value, value)
        self.assertLessEqual(value, max_value)

    def assertNotBetween(self, min_value, max_value, value):
        self.assertTrue(value < min_value or max_value < value)

    def assertClose(self, expected, actual, error=0.001):
        if type(expected) in (list,tuple):
            for x,y in itertools.izip(expected, actual):
                self.assertEqual(len(expected), len(actual))
                self.assertClose(x, y, error=error)
        else:
            self.assertBetween(expected-error, expected+error, actual)
