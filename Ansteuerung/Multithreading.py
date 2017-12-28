from time import sleep
from threading import Thread
import RPi.GPIO as GPIO
GPIO.setmode(GPIO.BCM)

#Endschalter
endZ = 4

#GPIOs in Array ordnen
step = [16, 19]
direct = [12, 26]
enable = [21, 13]

#Motoren definieren
Mot1 = 0
Mot2 = 1

#GPIOs initialisieren
GPIO.setup(step[Mot1], GPIO.OUT)
GPIO.setup(direct[Mot1], GPIO.OUT)
GPIO.setup(enable[Mot1], GPIO.OUT)
GPIO.setup(step[Mot2], GPIO.OUT)
GPIO.setup(direct[Mot2], GPIO.OUT)
GPIO.setup(enable[Mot2], GPIO.OUT)

GPIO.setup(endZ, GPIO.IN)

GPIO.output(step[Mot1], False)
GPIO.output(direct[Mot1], True)
GPIO.output(enable[Mot1], True)
GPIO.output(step[Mot2], False)
GPIO.output(direct[Mot2], True)
GPIO.output(enable[Mot2], True)



def turn(motor, speed, steps, direction):
    if direction == 1:
        GPIO.output(direct[motor], True)
    else:
        GPIO.output(direct[motor], False)

    GPIO.output (enable[motor], False)
    for i in range(0, steps):
        GPIO.output(step[motor], True)
        sleep(speed)
        GPIO.output(step[motor], False)
        
    GPIO.output(enable[motor], True)
    return

#while GPIO.input(endZ) == GPIO.HIGH:
Thread(target=turn, args=(Mot1, 0.0005, 4400, 0,) ).start()
Thread(target=turn, args=(Mot2, 0.0005, 4400, 1,) ).start()


#while True:
    #print (str(GPIO.input(endZ)))
    #if GPIO.input(endZ) == GPIO.HIGH:
        #turn (0.00005, 1, 0)
    
#GPIO.cleanup()
