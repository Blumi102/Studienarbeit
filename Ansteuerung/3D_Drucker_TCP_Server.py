import socket

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(("0.0.0.0", 5000))
server.listen(100)

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

            f = open("/home/pi/Documents/Test.xml", "w")
            f.write(nachricht_str)
            f.close()
            
finally:
    server.close()
