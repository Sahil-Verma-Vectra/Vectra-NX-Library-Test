using System;
using System.Collections.Generic;
using System.Linq;
using NXOpen.UF;
using NXOpen;
using Vectra.NX.ThreeD;
using Vectra.NX;
using NXOpen.Assemblies;

namespace Vectra_NX_Library_Testing
{
    public class Program
    {
        public static int GetUnloadOption(string dummy)
        {
            return (int)Session.LibraryUnloadOption.Immediately;
        }
        public static void Main(string[] args)
        {
            Session session = NXSupport.NXSession;
            Part workPart = NXSupport.WorkPart;
            VctPart vctPart = new VctPart(workPart);

            ListingWindow lw = session.ListingWindow;
            lw.Open();
            lw.WriteLine("**************************Listing Window Opened Successfully**************************");

            List<Body> Bodyobj = new List<Body>();
            List<Face> FaceObj = new List<Face>();
            List<Edge> EdgeObj = new List<Edge>();
            
            Component[] Components = vctPart.Part.ComponentAssembly.RootComponent.GetChildren();
            List<VctComponent> vctComponents = new List<VctComponent>();
            List<VctBody> vctBodies = new List<VctBody>();
            List<VctFace> vctFaces = new List<VctFace>();
            List<VctEdge> vctEdges = new List<VctEdge>();

            foreach (Component Component in Components)
            {
                vctComponents.Add(new VctComponent(Component));
                VctPart part = new VctPart((Part)Component.Prototype);
                part.LoadPartFully();
                
                foreach (Body body in part.Bodies.ToArray())
                {
                    Body objOccurrencebody = (Body)Component.FindOccurrence(body);

                    Bodyobj.Add(objOccurrencebody);
                    if (objOccurrencebody != null)
                    {
                        vctBodies.Add(new VctBody(objOccurrencebody));
                    }

                    foreach (Face face in body.GetFaces())
                    {
                        FaceObj.Add(face);
                        Face objFace = (Face)Component.FindOccurrence(face);
                        if (objFace != null)
                        {
                            vctFaces.Add(new VctFace(objFace));
                        }

                        foreach (Edge edge in face.GetEdges())
                        {
                            Edge edges = (Edge)Component.FindOccurrence(edge);
                            EdgeObj.Add(edge);
                            if (edges != null)
                            {
                                vctEdges.Add(new VctEdge(edges)); 
                            }
                        }
                    }
                }
            }
            lw.WriteLine("BodyList: " + Bodyobj.Count.ToString());
            lw.WriteLine("FaceList: " + FaceObj.Count.ToString());
            lw.WriteLine("EdgeList: " + EdgeObj.Count.ToString());
            lw.WriteLine("**************************************");
            lw.WriteLine("VctBodyList: " + vctBodies.Count.ToString());
            lw.WriteLine("VctFaceList: " + vctFaces.Count.ToString());
            lw.WriteLine("VctEdgeList: " + vctEdges.Count.ToString());
        }
    }
}
