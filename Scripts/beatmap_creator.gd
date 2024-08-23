extends Node

const SAVE_BEATMAP_PATH := "res://Beatmaps/testBeatmap.json"

var saveString := ""

var readString := ""

var songString := ""

var selectedSong := ""

var _file := File.new();

var timeSkipValue := -5

#Scenes
onready var initialScene := get_node("Initial")
onready var musicSelectScene := get_node("MusicSelect")
onready var changeTimeScene := get_node("ChangeTime")
onready var beatmapEditorScene := get_node("BeatmapEditor")

onready var songPlayButton := get_node("BeatmapEditor/PlayButton")
onready var beatSkipButton := get_node("BeatmapEditor/BeatSkipButton")

onready var musicContainer := get_node("MusicSelect/MusicScroll/MusicContainer")
onready var musicPlayer := get_node("MusicPlayer")

# Called when the node enters the scene tree for the first time.
func _ready():
	dir_contents("res://TempAssets/Music/")
	SetupTimeSkipButtons()
	pass # Replace with function body.

func dir_contents(path):
	var split_file_name
	var dir = Directory.new()
	if dir.open(path) == OK:
		dir.list_dir_begin()
		var file_name = dir.get_next()
		while file_name != "":
			if dir.current_is_dir():
				print("Found directory: " + file_name)
			else:
				print("Found file: " + file_name)
				split_file_name = file_name.split(".")
				if split_file_name[split_file_name.size() - 1] != "import":
					var songButton = Button.new()
					songButton.text = split_file_name[0]
					songButton.connect("pressed", self, "selectSong", ["res://TempAssets/Music/" + file_name])
					musicContainer.add_child(songButton)
			file_name = dir.get_next()
	else:
		print("An error occurred when trying to access the path.")
		

func selectSong(song):
	selectedSong = song
	updateSong()
	musicPlayer.play()

func updateSong() -> void:
	musicPlayer.stream = load(selectedSong)
	pass

		
#Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func save_beatmap() -> void:
	var error := _file.open(SAVE_BEATMAP_PATH, File.WRITE)
	if error != OK:
		printerr("Couldn't open file")
		return
		
	var data := {
		"lineText": saveString
	}
	
	var json_string := JSON.print(data)
	_file.store_string(json_string)
	_file.close()
	
func load_beatmap() -> void:
	var error := _file.open(SAVE_BEATMAP_PATH, File.READ)
	if error != OK:
		printerr("Couldn't open file")
		return
		
	var content := _file.get_as_text()
	_file.close()
	
	var data: Dictionary = JSON.parse(content).result
	readString = data.lineText
	

#Screen Displays
func DisplayInitialisation(var state : bool):
	initialScene.visible = state

func DisplayMusicSelect(var state : bool):
	musicSelectScene.visible = state

func DisplayChangeTime(var state : bool):
	changeTimeScene.visible = state

func DisplayBeatmapEditor(var state : bool):
	beatmapEditorScene.visible = state

#DON'T LOOK THIS IS EW
func SetupTimeSkipButtons():
	for timeSkipButtonValue in get_node("ChangeTime").get_children():
		var currentNode := timeSkipButtonValue as Node
		if(currentNode.is_class("Button")):
			var buttonNode := timeSkipButtonValue as Button
			if(buttonNode.name[0] == "R"):
				buttonNode.connect("pressed", self, "SetTimeSkipValue", [buttonNode.text, true])
			elif(buttonNode.name[0] == "F"):
				buttonNode.connect("pressed", self, "SetTimeSkipValue", [buttonNode.text, false])

func SetTimeSkipValue(var value : String, var isRewind : bool):
	if isRewind:
		timeSkipValue = -value.to_int()
		beatSkipButton.text = "Rewind: " + value
	else:
		timeSkipValue = value.to_int()
		beatSkipButton.text = "Forward: " + value
	
	DisplayChangeTime(false)
	DisplayBeatmapEditor(true)

#Button Presses

func _on_PlayButton_pressed():
	if !musicPlayer.playing:
		musicPlayer.playing = true
		songPlayButton.text = "Pause"
		return
	
	if musicPlayer.stream_paused:
		musicPlayer.stream_paused = false
		songPlayButton.text = "Pause"
		return
		
	musicPlayer.stream_paused = true
	songPlayButton.text = "Play"

func _on_BackToMainButton_pressed():
	get_tree().change_scene("res://Scenes/main_menu.tscn")


func _on_ChooseMusicButton_pressed():
	DisplayInitialisation(false)
	DisplayMusicSelect(true)


func _on_LoadBeatmapButton_pressed():
	DisplayInitialisation(false)


func _on_SelectSongButton_pressed():
	musicPlayer.playing = false
	DisplayMusicSelect(false)
	DisplayBeatmapEditor(true)


func _on_MusicSelectBackButton_pressed():
	musicPlayer.playing = false
	DisplayMusicSelect(false)
	DisplayInitialisation(true)

func _on_BeatmapEditorBackButton_pressed():
	musicPlayer.playing = false
	DisplayBeatmapEditor(false)
	DisplayInitialisation(true)

func _on_ChangeTimeSkipButton_pressed():
	if musicPlayer.playing && !musicPlayer.stream_paused:
		musicPlayer.stream_paused = true
		songPlayButton.text = "Play"
	
	DisplayBeatmapEditor(false)
	DisplayChangeTime(true)
