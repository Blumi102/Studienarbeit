from time import sleep
from threading import Thread
from xml.dom import minidom
import RPi.GPIO as GPIO
import socket
GPIO.setmode(GPIO.BCM) #GPIO Nummerierung im BCM-Mode
    
#TCP INIT
server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(("0.0.0.0", 8000))
server.listen(100)

#GPIOs in Array ordnen [MotR, MotL, MotT]
step = [16, 19, 23]
direct = [12, 26, 18]
enable = [21, 13, 24]

#Motoren INIT
MotR = 0 #rechts
MotL = 1 #links
MotT = 2 #Tisch

#Endschalter INIT
endX = 17
endY = 27
endZ = 22

#Motor GPIOs initialisieren
GPIO.setup(step[MotR], GPIO.OUT)
GPIO.setup(direct[MotR], GPIO.OUT)
GPIO.setup(enable[MotR], GPIO.OUT)
GPIO.setup(step[MotL], GPIO.OUT)
GPIO.setup(direct[MotL], GPIO.OUT)
GPIO.setup(enable[MotL], GPIO.OUT)
GPIO.setup(step[MotT], GPIO.OUT)
GPIO.setup(direct[MotT], GPIO.OUT)
GPIO.setup(enable[MotT], GPIO.OUT)

GPIO.output(step[MotR], False)
GPIO.output(direct[MotR], True)
GPIO.output(enable[MotR], True)
GPIO.output(step[MotL], False)
GPIO.output(direct[MotL], True)
GPIO.output(enable[MotL], True)
GPIO.output(step[MotT], False)
GPIO.output(direct[MotT], True)
GPIO.output(enable[MotT], True)

#Enschalter GPIOs initialisieren
GPIO.setup(endX, GPIO.IN)
GPIO.setup(endY, GPIO.IN)
GPIO.setup(endZ, GPIO.IN)


GPIO.output (enable[MotL], False)
GPIO.output(enable[MotR], False)
GPIO.output(enable[MotT], False)

#Ansteuerung Hauptfunktion für Motoren           #length in mm
def turn(motor, speed, length, direction):
    if direction == 1:
        GPIO.output(direct[motor], True)
    else:
        GPIO.output(direct[motor], False)

    if motor == MotT:
        steps = int(length*400) #1mm = 400 steps
    else:
        steps = int(length*53.198) #1mm = 53,198 steps
    for i in range(0, steps):
        GPIO.output(step[motor], True)
        sleep(speed)
        GPIO.output(step[motor], False)
    return

#Homing
def homing(axis):
    
    if axis == 'X':
        GPIO.output(direct[MotL], True)
        GPIO.output(direct[MotR], False)
    elif axis == 'Y':
        GPIO.output(direct[MotL], False)
        GPIO.output(direct[MotR], False)
    elif axis == 'Z':
        GPIO.output(direct[MotT], True)
    
    for i in range(0, 400000):
        if ((axis == 'X') and (GPIO.input(endX) == GPIO.HIGH)) or ((axis == 'Y') and (GPIO.input(endY) == GPIO.HIGH)):
            GPIO.output(step[MotL], True)
            GPIO.output(step[MotR], True)
            sleep(0.0008)
            GPIO.output(step[MotL], False)
            GPIO.output(step[MotR], False)

        elif ((axis == 'Z') and (GPIO.input(endZ) == GPIO.HIGH)):
            GPIO.output(step[MotT], True)
            sleep(0.0003)
            GPIO.output(step[MotT], False)

        else:
            return
    return

#Ansteuerung x-y-Achsenkreuz
def XYaxis(lengthX, lengthY, speed):

    lengthL = lengthX - lengthY
    lengthR = lengthX + lengthY

    if lengthL > 0:
        dirMotL = 0
    else:
        dirMotL = 1
        lengthL = abs(lengthL)

    if lengthR > 0:
        dirMotR = 1
    else:
        dirMotR = 0
        lengthR = abs(lengthR)

    if (lengthL > lengthR) & (lengthR != 0):
        speedL = speed
        speedR = speed * lengthL/lengthR
    elif (lengthL > lengthR) & (lengthR == 0):
        speedL = speed
        speedR = 0
    elif (lengthL < lengthR) & (lengthL != 0):
        speedR = speed
        speedL = speed * lengthR/lengthL
    else:
        speedR = speed
        speedL = 0

    Thread(target=turn, args=(MotL, speedL, lengthL, dirMotL,) ).start()
    Thread(target=turn, args=(MotR, speedR, lengthR, dirMotR,) ).start()

#Ansteuerung z-Achse
forward = 1
backward = 0
def Zaxis(length, speed):
    if length > 0:
        dirMotT = 0
    else:
        dirMotT = 1
    Thread(target=turn, args=(MotT, speed, abs(length), dirMotT,) ).start()

#TCP Verbindungsaufbau
try:
    while True:
        print("Waiting for a client...")
        komm, addr = server.accept()
        while True:
            nachricht = komm.recv(1024)

            if not nachricht:
                komm.close()
                break

            nachricht_str = nachricht.decode(encoding = 'UTF-8')
            print("[%s] %s" % (addr[0], nachricht_str))

            f = open("/home/pi/Dokumente/3D_Drucker/commands.xml", "w")
            f.write(nachricht_str)
            f.close()
            
            #XML-Reader
            xml = minidom.parse('/home/pi/Dokumente/3D_Drucker/commands.xml')
            commandlist = xml.getElementsByTagName('Command')
                   
            if (int(commandlist[0].attributes['X'].value) == -1000) | (int(commandlist[0].attributes['Y'].value) == -1000) | (int(commandlist[0].attributes['Z'].value) == -1000): #homing
                if int(commandlist[0].attributes['Z'].value) == -1000:
                    Thread(target=homing, args=('Z',) ).start()
                if int(commandlist[0].attributes['X'].value) == -1000:
                    homing("X")
                if int(commandlist[0].attributes['Y'].value) == -1000:
                    homing("Y")
            else:                                                                                                                                       #normale Ansteuerung
                XYaxis(int(commandlist[0].attributes['X'].value), int(commandlist[0].attributes['Y'].value), 0.0002)
                Zaxis(int(commandlist[0].attributes['Z'].value), 0.0003)
            
finally:
    server.close()
    GPIO.cleanup()
