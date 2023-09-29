from socket import socket
from socket import SOCK_STREAM  # TCP
from socket import AF_INET      # IPv4
from threading import Thread
import random
from operation import Operation, Result
import json
import traceback

host = 'localhost'
port = 55521
server = socket(AF_INET,SOCK_STREAM)
server.bind((host,port))
server.listen(4)

def add_cmd(a,b):
    calculation = a+b
    result = Result(float(calculation))
    return json.dumps({
         "result": result._result,
    })

def subtract_cmd(a,b):
    calculation = a-b
    result = Result(float(calculation))
    return json.dumps({
         "result": result._result,
    })
    
def random_cmd(a,b):
    if a > b:
        temp = a
        a = b
        b = temp
    b += 1
    calculation = random.randrange(a,b,1)
    result = Result(float(calculation))
    return json.dumps({
         "result": result._result,
    })

def get_input(client):
    jsondata = client.recv(1024).decode()
    jsonobject = json.loads(jsondata)
    operation = Operation(jsonobject['command'], jsonobject['number1'], jsonobject['number2'])
    return operation

def handle(client):
    while True:
        result = 0
        try:
            operation = get_input(client)
            m,a,b = operation._command, operation._number1,operation._number2
            match m:
                case 'Add':
                    result = add_cmd(a,b)
                case 'Subtract':
                    result = subtract_cmd(a,b)
                case 'Random':
                    result = random_cmd(a,b)  
                case _:
                      handle(client)
            client.send(result.encode())
        except Exception:
            traceback.print_exc()
            client.close()
            break

def recieve():
    while True:
       client,addr = server.accept()
       client.send('Forbundet'.encode())
       Thread(target=handle,args=(client,)).start()


print('Starting server...')
recieve()
