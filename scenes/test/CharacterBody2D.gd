extends CharacterBody2D


const SPEED = 300.0
const JUMP_VELOCITY = -400.0

# Get the gravity from the project settings to be synced with RigidBody nodes.
#var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")
func get_input():
	#keyboard input
	var input_dir = Input.get_vector("left",
	 "right",
	 "up",
	 "down")

func _physics_process(_delta):
	#движение перса
	get_input()
	move_and_slide()
