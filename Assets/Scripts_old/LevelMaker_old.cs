using System;

public class LevelMaker {

	public int[,] blockTypeArray;
	public int[,] oneShot;
	public int[,] fastGame;
	public int[,] allNormal;
	public int[,] miscBlocks;
	public double[,,] blockPosArray;

	// public double initPosX;
	// public double initPosY;
	// public double padding;
	// public double[] blockSize;

	public LevelMaker(){
		miscBlocks = new int[,] {
			{0, 3, 2, 1, 2, 1, 2, 3, 0},
			{3, 2, 1, 1, 1, 1, 1, 2, 3},
			{3, 2, 1, 1, 1, 1, 1, 2, 3},
			{0, 3, 2, 1, 2, 1, 2, 3, 0}
			};
		
		allNormal = new int[,] {
			{1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1}
			};

		fastGame = new int[,] {
			{0, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 1, 1, 1, 1, 1, 1, 1, 0}
			};
		
		oneShot = new int[,] {
			{0, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 0, 0, 0, 0, 0, 0, 0, 0},
			{0, 0, 0, 0, 1, 0, 0, 0, 0}
			};
		blockTypeArray = fastGame;

		blockPosArray = new double[,,] {
			{{-2.4, 1.8}, {-1.8, 1.8}, {-1.2, 1.8}, {-0.6, 1.8}, {0.0, 1.8}, {0.6, 1.8}, {1.2, 1.8}, {1.8, 1.8}, {2.4, 1.8}},
			{{-2.4, 1.4}, {-1.8, 1.4}, {-1.2, 1.4}, {-0.6, 1.4}, {0.0, 1.4}, {0.6, 1.4}, {1.2, 1.4}, {1.8, 1.4}, {2.4, 1.4}},
			{{-2.4, 1.0}, {-1.8, 1.0}, {-1.2, 1.0}, {-0.6, 1.0}, {0.0, 1.0}, {0.6, 1.0}, {1.2, 1.0}, {1.8, 1.0}, {2.4, 1.0}},
			{{-2.4, 0.6}, {-1.8, 0.6}, {-1.2, 0.6}, {-0.6, 0.6}, {0.0, 0.6}, {0.6, 0.6}, {1.2, 0.6}, {1.8, 0.6}, {2.4, 0.6}}
			};
			
		/* initPosX = -2.4;
		initPosY = 1.8;
		padding = 0.1;

		blockSize = new double[] {0.5, 0.3};

		int rowQnt = blockTypeArray.GetLength(0);
		int colQnt = blockPosArray.GetLength(1);

		for (int row = 0; row < rowQnt; row++)
		{
			for (int col = 0; col < colQnt; col++)
			{
				blockPosArray[row,col,0] = initPosX + (blockSize[0] * col) + padding;
				blockPosArray[row,col,1] = initPosY + (blockSize[1] * row) + padding;
			}
		} */
	}
}
