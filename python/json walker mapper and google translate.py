import json
from deep_translator import GoogleTranslator # pip install -U deep_translator
translator = GoogleTranslator(source='nl', target='en')

f = open("json.json", "r")
all =f.read()
data = json.loads(all)

def translate(x, key): #replace with whatever you want your mapper to do
	if key == "caption":
		print("translating: " + x)
		return translator.translate(x)
	else:
		return x

def walk(node, key):
	if type(node) is dict:
		return {k: walk(v, k) for k, v in node.items()} #dict		
	elif type(node) is list:
		return [walk(x, key) for x in node] #list
	else:
		return translate(node, key) #leaf
		
	
new = walk(data, None)

open("translated.json", "w").write(json.dumps(new))
