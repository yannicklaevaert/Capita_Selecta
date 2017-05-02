#!/usr/bin/env python
import sys, serial, os, csv
from dateutil import parser as dateparser
import time
import numpy

class Package(object):
    def __init__(self, package):
        if [d & 0x80 != 0 for d in package] != [False, True, True, True, True]:
           raise ValueError("Invalid data packet.")
        self.package = package
        self.data = [0] * 9

    def package_to_data(self):
        data = [1, 0, 0, 0, 0, 0, 0, 0, 0]
        for i in range(1,9):
            data[i] = self.package[i] - 128
        self.set_data(data)

    def get_data(self):
        return self.data

    def set_data(self, data):
        self.data = data

class CMS50Dplus(object):
    def __init__(self, port):
        self.port = port
        self.conn = None

    def isConnected(self):
        """Geeft altijd true"""
        return type(self.conn) is serial.Serial and self.conn.isOpen()


    def communicationPossible(self):
        """detecteert of kabel inzit, niet of device aanligt"""
        x= os.system("ls " + str(self.port))
        if x==0:
            return "connected"
        else:
            return "disconnected"

    def check_package(self, package):
        if len(package) == 9 and package[0] == 1:
            for byte in package[1:]:
                if byte < 128:
                    return False
            return True
        else:
            return False

    def read_package(self):
        return self.read_init_package(9)



    def handshake(self):
        serial.Serial.flush(self.conn)
        i = 0
        print "write init package"
        self.init_handshake()
        serial.Serial.flush(self.conn)
        for i in range(0, 10):
            self.second_handshake(i)


    def init_handshake(self):
        list = []
        list += self.make_write_package(162)
        list += self.make_write_package(167)
        list += self.make_write_package(168)
        list += self.make_write_package(169)
        list += self.make_write_package(170)
        list += self.make_write_package(176)
        self.conn.write(bytearray(list))

    def read_init_package(self, n):
        package = [0] * n
        idx = 0
        while idx < n:
            byte = self.getByte()
            package[idx] = byte
            idx += 1
        return package

    def second_handshake(self, i):
        second_list =   [167,   162,    160,    176,    172,    179,    168,    170,    169,    161,    161]
        size_list =     [48 ,   2,      2,      4,      8,      3,      17,     18,     9,      9]
        size = size_list[i]
        n = second_list[i]
        print "reading"
        print self.read_init_package(size)
        print "writing"
        write_package = bytearray(self.make_write_package(n))
        print self.make_write_package(n)
        self.conn.write(write_package)

    def polling_data(self):
        self.conn.write(bytearray(self.make_write_package(175)))

    def make_write_package(self, n):
        return [125, 129, n, 128, 128, 128, 128, 128, 128]

    def connect(self):
        if self.conn is None:
            self.conn = serial.Serial(port = self.port,
                                      baudrate = 115200,
                                      bytesize=serial.EIGHTBITS,
                                      parity = serial.PARITY_NONE,
                                      stopbits = serial.STOPBITS_ONE,
                                      xonxoff=1,
                                      timeout = None)
            self.conn.setDTR(1)
            self.handshake()
            print("handshake done, start data")



        elif not self.isConnected():
            self.conn.open()



    def disconnect(self):
        if self.isConnected():
            self.conn.close()

    def getByte(self):
        char = self.conn.read()
        if len(char) == 0:
            return None
        else:
            return ord(char)

    def sendBytes(self, values):
        return self.conn.write(''.join([chr(value & 0xff) for value in values]))

    # Waits until the specified byte is seen or a timeout occurs
    def expectByte(self, value):
        while True:
            byte = self.getByte()
            if byte is None:
                return False
            elif byte == value:
                return True


    def getLiveData(self):
        myfile = "foo.txt"
        #connect
        self.connect()
        package = self.read_package()
        print "package" , package
        heartbeat_old = 0
        while self.check_package(package):
            package =  self.read_package()
            data = self.parse_raw_package(package)
            heartbeat = data[5]
            if heartbeat != heartbeat_old:
            	self.write_data(data, myfile)
            	heartbeat_old = heartbeat

    def parse_raw_package(self, package):
        data = [0] * 9
        for i in range(1, 9):
            data[i] = package[i] - 128
        print data
        return data

    def write_data(self, data, filename):
        heartbeat = data[5]
        f = open(filename, 'w')
        f.write(str(heartbeat)) 



if __name__ == "__main__":
    filename = "liveData.csv"
    port = "/dev/ttyUSB0"
    print("Saving live data to " + filename)
    print("Press CTRL-C or disconnect the device to terminate data collection.")
    oximeter = CMS50Dplus(port)
    oximeter.getLiveData()
    # dumpLiveData(port, filename)
