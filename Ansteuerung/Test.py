from time import sleep
import RPi.GPIO as GPIO
GPIO.setmode(GPIO.BCM)
#GPIOs zuordnen
step = 16
direct = 12
enable = 21
endZ = 4

#GPIOs definieren
GPIO.setup(step, GPIO.OUT)
GPIO.setup(direct, GPIO.OUT)
GPIO.setup(enable, GPIO.OUT)
GPIO.setup(endZ, GPIO.IN)

GPIO.output(step, False)
GPIO.output(direct, True)
GPIO.output(enable, True)

def turn(speed, steps, direction):
    if direction == 1:
        GPIO.output(direct, True)
    else:
        GPIO.output(direct, False)

    GPIO.output (enable, False)
    for i in range(0, steps):
        GPIO.output(step, True)
        sleep(speed)
        GPIO.output(step, False)
        
    GPIO.output(enable, True)
    return

while GPIO.input(endZ) == GPIO.HIGH:
    turn (0.00005, 1, 0)

#while True:
    #print (str(GPIO.input(endZ)))
    #if GPIO.input(endZ) == GPIO.HIGH:
        #turn (0.00005, 1, 0)
    
GPIO.cleanup()
