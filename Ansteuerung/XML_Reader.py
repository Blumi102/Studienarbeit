from xml.dom import minidom

xml = minidom.parse('/home/pi/Documents/Test.xml')
commandlist = xml.getElementsByTagName('Command')

print(len(commandlist))
print(commandlist[0].attributes['X'].value)
print(commandlist[0].attributes['Y'].value)
print(commandlist[0].attributes['Z'].value)

#http://www.willemer.de/informatik/python/xml.htm
#for s in itemlist :
#    print(s.attributes['X'].value+":",
#            s.childNodes[0].nodeValue)
