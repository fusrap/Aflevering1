from socket import socket
from socket import SOCK_STREAM  # TCP
from socket import AF_INET      # IPv4
from threading import Thread
import random

host = 'localhost'
port = 55521
server = socket(AF_INET,SOCK_STREAM)
server.bind((host,port))
server.listen(4)

def add(a,b):
    return a+b

def subtract(a,b):
    return a-b;

def get_input(client):
    message = client.recv(1024).decode()
    message = message.split(';',2)
    print(message)
    return str(message[0]), float(message[1]),float(message[2])

def handle(client):
    while True:
        result = 0
        try:
            m,a,b = get_input(client)
            match m:
                case 'Add':
                    result = add(a,b)
                case 'Subtract':
                    result = subtract(a,b)
                case 'Random':
                    b += 1
                    result = random.randrange(a,b,1)
                case _:
                      handle(client)
            print(result)
            client.send(f'{result}'.encode())
        except:
            client.close()
            break

def recieve():
    while True:
       client,addr = server.accept()
       client.send('Forbundet'.encode())
       Thread(target=handle,args=(client,)).start()


print('Starting server...')
recieve()
