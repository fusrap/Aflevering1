from socket import socket
from socket import SOCK_STREAM  # TCP
from socket import AF_INET      # IPv4

server = 'localhost'
port = 55521
clientSocket = socket(AF_INET, SOCK_STREAM) 
clientSocket.connect((server,port))

def get_user_cmd():
    while True:
        cmd = input('Indtast operation \'Random\',\'Add\' eller \'Substract\': ').strip().lower().capitalize()
        if cmd in ["Add", "Subtract", "Random"]:
            return cmd
        else:
            print('Ugyldig operation, prøv igen')

def get_user_number():
    while True:
        user_input = input(': ')
        try:
            return float(user_input)  
        except ValueError:
            print('Du skal indtaste et tal',end='')

def process_operation(parameteres):
    request = ''
    for item in parameteres:
        request += f'{item};'
    return request[0].upper() + request[1:-1]
        

message = clientSocket.recv(1024).decode()
print(message)
while True:
    parameteres = []
    parameteres.append(get_user_cmd())
    print('Indtast det første tal',end='')
    parameteres.append(get_user_number())
    print('Indtast det andet tal',end='')
    parameteres.append(get_user_number())

    request = process_operation(parameteres);
    clientSocket.send(request.encode())
    result = clientSocket.recv(1024).decode()
    print(f'Result: {result}')

