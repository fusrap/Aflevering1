class Operation:
    def __init__(self, command, number1, number2):
        self._command = command
        self._number1 = number1
        self._number2 = number2

    @property
    def command(self):
        return self._command

    @command.setter
    def command(self, value):
        self._command = value

    @property
    def number1(self):
        return self._number1

    @number1.setter
    def number1(self, value):
        self._number1 = value

    @property
    def number2(self):
        return self._number2

    @number2.setter
    def number2(self, value):
        self._number2 = value

class Result:
    def __init__(self, result):
        self._result = result

    @property
    def result(self):
        return self._number2

    @result.setter
    def result(self, value):
        self._number2 = value