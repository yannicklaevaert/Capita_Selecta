#!/usr/bin/env python
import sys, serial, os, csv
from dateutil import parser as dateparser
import time

class LiveDataPoint(object):
    def __init__(self, time, data):
        if [d & 0x80 != 0 for d in data] != [True, False, False, False, False]:
           raise ValueError("Invalid data packet.")

        self.time = time

        # 1st byte
        self.signalStrength = data[0] & 0x0f
        self.fingerOut = bool(data[0] & 0x10)
        self.droppingSpO2 = bool(data[0] & 0x20)
        self.beep = bool(data[0] & 0x40)

        # 2nd byte
        self.pulseWaveform = data[1]

        # 3rd byte
        self.barGraph = data[2] & 0x0f
        self.probeError = bool(data[2] & 0x10)
        self.searching = bool(data[2] & 0x20)
        self.pulseRate = (data[2] & 0x40) << 1

        # 4th byte
        self.pulseRate |= data[3] & 0x7f

        # 5th byte
        self.bloodSpO2 = data[4] & 0x7f

    def getBytes(self):
        result = [0]*5

        # 1st byte
        result[0] = self.signalStrength & 0x0f
        if self.fingerOut:
            result[0] |= 0x10
        if self.droppingSpO2:
            result[0] |= 0x20
        if self.beep:
            result[0] |= 0x40
        result[0] |= 0x80 # sync bit

        # 2nd byte
        result[1] = self.pulseWaveform & 0x7f

        # 3rd byte
        result[2] = self.barGraph & 0x0f
        if self.probeError:
            result[2] |= 0x10
        if self.searching:
            result[2] |= 0x20
        result[2] |= (self.pulseRate & 0x80) >> 1

        # 4th byte
        result[3] = self.pulseRate & 0x7f

        # 5th byte
        result[4] = self.bloodSpO2 & 0x7f

        return result

    def __repr__(self):
        hexBytes = ['0x{0:02X}'.format(byte) for byte in self.getBytes()]
        return "LiveDataPoint({0}, [{1}])".format(self.time.__repr__(), ', '.join(hexBytes))

    def __str__(self):
        return ", ".join(["Time = {0}",
                          "Signal Strength = {1}",
                          "Finger Out = {2}",
                          "Dropping SpO2 = {3}",
                          "Beep = {4}",
                          "Pulse waveform = {5}",
                          "Bar Graph = {6}",
                          "Probe Error = {7}",
                          "Searching = {8}",
                          "Pulse Rate = {9} bpm",
                          "SpO2 = {10}%"]).format(self.time,
                                                  self.signalStrength,
                                                  self.fingerOut,
                                                  self.droppingSpO2,
                                                  self.beep,
                                                  self.pulseWaveform,
                                                  self.barGraph,
                                                  self.probeError,
                                                  self.searching,
                                                  self.pulseRate,
                                                  self.bloodSpO2)

    @staticmethod
    def getCsvColumns():
        return ["Time", "PulseRate", "SpO2", "PulseWaveform", "BarGraph",
                "SignalStrength", "Beep", "FingerOut", "Searching",
                "DroppingSpO2", "ProbeError"]

    def getCsvData(self):
        return [self.time, self.pulseRate, self.bloodSpO2, self.pulseWaveform,
                self.barGraph, self.signalStrength, self.beep,
                self.fingerOut, self.searching, self.droppingSpO2,
                self.probeError]

    def getDictData(self):
        ret = dict()
        for n, d in zip(self.getCsvColumns(), self.getCsvData()):
            ret[n] = d
        return ret


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
            for byte in package:
                if byte < 128:
                    return False
            return True
        else:
            return False

    def read_package(self):
        package = []
        byte = self.getByte()
        package.append(byte)
        byte = self.getByte()
        while byte != None and byte >= 128:
            package.append(byte)
            byte = self.getByte()
            print byte
        #return package + byte of next package
        return package, byte



    def handshake(self):
        serial.Serial.flush(self.conn)
        i = 0
        print "write init package"
        self.init_handshake()
        for i in range(0, 10):
            print i
            self.second_handshake(i)
        print(self.getByte())





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
        second_list =   [167,   162,    160,    176,    172,    179,    168,    170,    169,    161]
        size_list =     [40 , 8,   2,      2,      4,      8,      3,      18,     18,     9,      9]
        size = size_list[i]
        n = second_list[i]
        print self.read_init_package(size)
        self.conn.write(bytearray(self.make_write_package(n)))

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
            print "done connecting"

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
        #connect
        self.connect()

        # # Zolang packet niet start met 1
        # # dan nog geen goede data
        # # lees: random shit happens
        # start_byte = False
        # while not start_byte:
        #     self.second_handshake()
        #     byte = self.getByte()
        #     if byte is None:
        #         break
        #     if byte == 0x01:
        #         start_byte = True
        #         print "Start measuring"
        #
        # # maak leeg packet van 5 keer 0
        # packet = [0] * 9
        # idx = 0
        # #lees data in
        # while start_byte:
        #     # break als je niets meer leest
        #     if byte is None:
        #         break
        #
        #     #alle bytes hebben meest linkse bit als "controlebit"
        #     #lees: check meest linkse bit == 1
        #     if byte >= 128:
        #         #als packet vol is
        #         if idx == 9:
        #             # en eerste bit is gezet
        #             if packet[0] == 0x01:
        #                 print("Goed packet", packet)
        #                 #maak dataobject aan
        #                 # yield LiveDataPoint(datetime.datetime.utcnow(), packet)
        #                 #stuur om volgend packet
        #                 # self.polling_data()
        #                 packet = [0]*9
        #                 idx = 0
        #                 # lees byte en sla op
        #                 byte = self.getByte()
        #             else:
        #                 print("ongeldig packet? ", packet)
        #                 packet = [0] * 9
        #                 idx = 0
        #                 byte = self.getByte()
        #
        #         else:
        #             #vul packet
        #             packet[idx] = byte
        #             idx += 1
        #             byte = self.getByte()
        #
        #     elif byte == 1:
        #         packet = [0] * 9
        #         idx = 0
        #         packet[idx] = byte
        #         byte = self.getByte()
        #     else:
        #         packet = [0] * 9
        #         idx = 0
        #         packet[idx] = byte
        #         byte = self.getByte()





def dumpLiveData(port, filename):
    """
    Open file
    maakt oximeter object aan
    roep oximeter.getLiveData op
    """
    port = port
    oximeter = CMS50Dplus(port)
    measurements = 0
    with open(filename, 'w') as csvfile:
        writer = csv.writer(csvfile, quoting=csv.QUOTE_NONNUMERIC)
        writer.writerow(LiveDataPoint.getCsvColumns())
        for liveData in oximeter.getLiveData():
            print(liveData)
            writer.writerow(liveData.getCsvData())
            measurements += 1
            sys.stdout.write("\rGot {0} measurements...".format(measurements))
            sys.stdout.flush()


if __name__ == "__main__":
    filename = "liveData.csv"
    port = "/dev/ttyUSB0"
    print("Saving live data to " + filename)
    print("Press CTRL-C or disconnect the device to terminate data collection.")
    oximeter = CMS50Dplus(port)
    oximeter.connect()
    # dumpLiveData(port, filename)
