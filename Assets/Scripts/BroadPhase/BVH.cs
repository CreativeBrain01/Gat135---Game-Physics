using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BVH : BroadPhase
{
	BVHNode rootNode;

	public override void Build(AABB aabb, List<Body> bodies)
	{
		potentialCollisionCount = 0;
		List<Body> sorted = new List<Body>(bodies);

		
		// sort bodies along x-axis (position.x)
		/*< https://stackoverflow.com/questions/24187287/c-sharp-listt-orderby-float-member>*/
		//sorted.Sort();
		/*sorted = */sorted.OrderBy(body => body.transform.position.x).ToList();
// set sorted bodies to root bvh node
		rootNode = new BVHNode(sorted);
	}

	public override void Query(AABB aabb, List<Body> bodies)
	{
		rootNode.Query(aabb, bodies);
		// update the number of potential collisions
		potentialCollisionCount = potentialCollisionCount + bodies.Count;
	}

	public override void Query(Body body, List<Body> bodies)
	{
		Query(body.shape.aabb, bodies);
	}

	public override void Draw()
	{
		rootNode?.Draw();
/*< check if the root node is not null below, use ? operator>
 
		 < call Draw() on root node >*/
	}
}

