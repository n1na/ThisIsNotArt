using System;

namespace ThisIsNotArt.Model
{
	public class BoxData
	{
		#region Properties
		/// <summary>
		/// Box id - needed for search
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Box left position
		/// </summary>
		public int Left { get; set; }

		/// <summary>
		/// Box width and height
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Text to show in the box
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Box top position
		/// </summary>
		public int Top { get; set; }

		/// <summary>
		/// Url the box leads to
		/// </summary>
		public string Url { get; set; }
		#endregion Properties

		#region Functions
		/// <summary>
		/// Calculates the horizontal center of the box
		/// </summary>
		/// <returns>Integer representing box X position</returns>
		public int CenterX() 
		{ 
			return Left + (Size/2); 
		}

		/// <summary>
		/// Calculates the vertical center of the box
		/// </summary>
		/// <returns>Integer representing box Y position</returns>
		public int CenterY()
		{
			return Top + (Size / 2);
		}

		/// <summary>
		/// Checks if the current box overlaps an existing one
		/// </summary>
		/// <returns>True if current box coordinates overlape existing ones</returns>
		public bool CheckDistance(int left, int top, int size)
		{
			int siblingBoxCenterX = left + (size / 2);
			int siblingBoxCenterY = top + (size / 2);

			int relativeX = siblingBoxCenterX - CenterX();
			int relativeY = siblingBoxCenterY - CenterY();

			// Pitagora's theorem: c^2 = a^2 + b^2
			var distance = Math.Sqrt(Math.Pow(relativeX, 2) + Math.Pow(relativeY, 2));
			var minimumDistance = Math.Sqrt(Math.Pow((Size / 2 + size / 2), 2) * 2); // refer to ASCII below for a better understanding

			return distance < minimumDistance;
		}
		#endregion Functions

		#region ASCII
		/*
		 *        ?YJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJY^                                             
                  &5^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^!@!                                             
                  #J                                :@!                                             
                  #J                                :@!                                             
                  #J                                :@!                                             
                  #J                                :@!                                             
                  #J                                :@!                                             
                  #J                                :@!                                             
                  #J               ^.               :@!                                             
                  #J              ^@B?.             :@!                                             
                  #J              ^@7YGJ:           :@!                                             
                  #J              ^@^ :?G5^         :@!                                             
                  #J              ^@~    !GP~       :@!                                             
                  #J      1/2     ^@~      !PG!     :@!                                             
                  #J              ^@~        ~PP?:  :@!                                             
                  #J              ^@^          .?GY:.@!                                             
                  &P~~~~~~~~~~~~~~7@?~~~~~~~~~~~^!P&P@7 ..........................                  
                  7J??????????????Y@Y???????????????JB&BYYY5555555555555555555555#7                 
                                  ^@^                J@PP?.                     .&J                 
                                  ^@~                Y# ^YGJ.                   .&?                 
                                  ^@~                Y#   :JGY:                 .&?                 
                                  ^@~                Y#     .?GY^               .&?                 
                                  ^@^                J#        7PP!             .&?                 
                                  ^@7^^^^^^^^^^^^^^^^5&^^^^^^^^^^?#B7.          .&?                 
                                  .JJJJJJJJJJJJJJJJJJG&JJJJJJJJJJJJYY:          .&?                 
                                                     J#                         .&?                 
                                                     Y#                         .&?                 
                                                     Y#                         .&?                 
                                                     Y#                         .&?                 
                                                     Y&::::::::::::::::::::::::::@J                 
                                                     7PYYYYYYYYYYYYYYYYYYYYYYYYYYP!                 
		 */
		#endregion ASCII
	}
}
