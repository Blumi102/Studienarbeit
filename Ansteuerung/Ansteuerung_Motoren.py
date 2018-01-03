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
endZ = 4

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
GPIO.setup(endZ, GPIO.IN)

#Ansteurung Hauptfunktion                  #length in mm
def turn(motor, speed, length, direction):
    if direction == 1:
        GPIO.output(direct[motor], True)
    else:
        GPIO.output(direct[motor], False)

    GPIO.output (enable[motor], False)
    if motor == MotT:
        steps = int(length*1600) #1mm = 1600 steps
    else:
        steps = int(length*53.198) #1mm = 52,914 steps
    for i in range(0, steps):
        GPIO.output(step[motor], True)
        sleep(speed)
        GPIO.output(step[motor], False)
        
    GPIO.output(enable[motor], True)
    return

#Ansteuerung x-Achse
forward = 1
backward = 0
def Xaxis(length, speed, direction):
    if direction == forward:
        dirMotL = 0
        dirMotR = 1
    elif direction == backward:
        dirMotL = 1
        dirMotR = 0
    else:
        print ("Error")
    Thread(target=turn, args=(MotL, speed, length, dirMotL,) ).start()
    Thread(target=turn, args=(MotR, speed, length, dirMotR,) ).start()

#Ansteuerung y-Achse
def Yaxis(length, speed, direction):
    Thread(target=turn, args=(MotL, speed, length, direction,) ).start()
    Thread(target=turn, args=(MotR, speed, length, direction,) ).start()

#Ansteuerung z-Achse
def Zaxis(length, speed, direction):
    if direction == forward:
        dirMotT = 0
    elif direction == backward:
        dirMotT = 1
    else:
        print ("Error")
    turn(MotT, speed, length, dirMotT)

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

            Yaxis(int(commandlist[0].attributes['Y'].value), 0.0002, backward)
            Zaxis(int(commandlist[0].attributes['Z'].value), 0.00005, backward)
            
finally:
    server.close()
       
#Xaxis(int(commandlist[0].attributes['X'].value), 0.0002, forward)
#Yaxis(100, 0.0002, forward)
#Zaxis(int(commandlist[0].attributes['Z'].value), 0.00005, forward)
#Zaxis(140, 0.00005, backward)

#for i in range(0, 100):
 #   Xaxis(10, 0.0002, forward)
  #  Yaxis(10, 0.0002, forward)

#while True:
    #print (str(GPIO.input(endZ)))
    #if GPIO.input(endZ) == GPIO.HIGH:
        #turn (0.00005, 1, 0)
    
#GPIO.cleanup()
