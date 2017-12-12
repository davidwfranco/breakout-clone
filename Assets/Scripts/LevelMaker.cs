
public class LevelMaker {

	public int[,] blockArray;

	// Use this for initialization
	void Start () {
		blockArray = new int[4,5] {{0, 1, 1, 1, 0}, {1, 1, 1, 1, 1}, {1, 1, 1, 1, 1}, {0, 1, 1, 1, 0}};
	}
}
