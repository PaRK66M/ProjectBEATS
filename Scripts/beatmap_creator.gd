extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	dir_contents("res://TempAssets/Music/")
	pass # Replace with function body.

func dir_contents(path):
	var dir = Directory.new()
	if dir.open(path) == OK:
		dir.list_dir_begin()
		var file_name = dir.get_next()
		while file_name != "":
			if dir.current_is_dir():
				print("Found directory: " + file_name)
			else:
				print("Found file: " + file_name.split(".")[0])
			file_name = dir.get_next()
	else:
		print("An error occurred when trying to access the path.")
		
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
