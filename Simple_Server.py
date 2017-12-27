import socket

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(("192.168.2.10", 5000))
server.listen(100)

try:                      
    while True:
        print("waiting for a client...")
        komm, addr = server.accept()
        while True:
            nachricht = komm.recv(1024)
                
            if not nachricht:
                komm.close()
                break

            nachricht_str = nachricht.decode(encoding='UTF-8')
            print("[%s] %s" % (addr[0], nachricht_str)) 
            antwort = input("Antwort: ")
            antwort_byte = str.encode(antwort)
            komm.send(antwort_byte)
finally: 
    server.close()
