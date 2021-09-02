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
                    if (objOccurrencebody != null)
                    {
                        vctBodies.Add(new VctBody(objOccurrencebody));
                        Bodyobj.Add(objOccurrencebody);

                        foreach (Face face in body.GetFaces())
                        {
                            Face objFace = (Face)Component.FindOccurrence(face);
                            vctFaces.Add(new VctFace(objFace));

                            foreach (Edge edge in face.GetEdges())
                            {
                                Edge edges = (Edge)Component.FindOccurrence(edge);
                                vctEdges.Add(new VctEdge(edges));
                            }
                        }
                    }
                }
                //int i = 0;
                //int j = 0;
                //foreach (VctBody vctBody in vctBodies)
                //{
                //    Body body = (Body)vctBody.Prototype;
                //    Body body2 = (Body)body.Prototype;
                //    if (body != null)
                //    {
                //        lw.WriteLine(body.JournalIdentifier);
                //    }
                //    else
                //    {
                //        lw.WriteLine("null");
                //    }

                //    lw.WriteLine(vctBody.Prototype.JournalIdentifier);
                //    lw.WriteLine(vctBody.body.Prototype.JournalIdentifier);
                //    if (vctBody.Prototype.JournalIdentifier != "" || vctBody.Prototype.JournalIdentifier != null)
                //    {
                //        i++;
                //    }
                //    if (vctBody.body.Prototype.JournalIdentifier != "" || vctBody.body.Prototype.JournalIdentifier != null)
                //    {
                //        j++;
                //    }
                //}
                //lw.WriteLine(i.ToString() + " " + j.ToString() + " " + vctBodies.Count.ToString());
            }

            //foreach (VctBody vctBody in vctBodies)
            //{
            //    vctBody.AllFacesUsingDirRayTracing(new double[3] { 0, 0, 0 }, new double[3] { 0, 1, 0 }, out VctFace[] faces);
            //    foreach (VctFace face1 in faces)
            //    {
            //        lw.WriteLine(face1.JournalIdentifier);
            //    }
            //}

            //for (int i = 0; i < vctBodies.Count; i++)
            //{
            //    List<KeyValuePair<VctFace, VctFace>> pairs = vctBodies[3].AllMatingFaces(vctPart, vctBodies[i]);
            //    foreach (KeyValuePair<VctFace, VctFace> pair in pairs)
            //    {
            //        lw.WriteLine(pair.Key.JournalIdentifier);
            //        lw.WriteLine(pair.Value.JournalIdentifier);
            //    }
            //    lw.WriteLine(pairs.Count.ToString());
            //    lw.WriteLine("----------------------------------------------------------------");
            //}

            //foreach (Body body in Bodyobj)
            //{
            //    vctBodies.Add(new VctBody(body));
            //    lw.WriteLine(body.JournalIdentifier);
            //    lw.WriteLine(body.IsSolidBody.ToString() + " ----> " + "Solid Body");
            //    lw.WriteLine(body.IsSheetBody.ToString() + " ----> " + "Sheet Body");
            //    Component comp = body.OwningComponent;
            //    lw.WriteLine(comp.DisplayName.ToString());
            //}

            //for (int i = 2; i < 15; i++)
            //{
            //    vctBodies.Add(new VctBody(Bodyobj[i]));
            //    lw.WriteLine(Bodyobj[i].JournalIdentifier);
            //    lw.WriteLine(Bodyobj[i].IsSolidBody.ToString() + "---->" + "Solid Body");
            //    lw.WriteLine(Bodyobj[i].IsSheetBody.ToString() + "---->" + "Sheet Body");
            //    Component comp = Bodyobj[i].OwningComponent;
            //    lw.WriteLine(comp.DisplayName.ToString());
            //}
            //List<VctBody> vctBodies1 = new List<VctBody>();
            //vctBodies1.Add(new VctBody(Body[1]));

            //List<VctBody> vctBodies2 = new List<VctBody>();
            //vctBodies1.Add(new VctBody(Body[3]));
            //foreach (Body body in Body)
            //{
            //    lw.WriteLine(body.NameLocation.X.ToString());
            //    lw.WriteLine(body.NameLocation.Y.ToString());
            //    lw.WriteLine(body.NameLocation.Z.ToString());
            //    lw.WriteLine("------------------------------------");
            //}

            //try
            //{
            //    lw.WriteLine(vctFaces[1].JournalIdentifier);
            //    lw.WriteLine(vctFaces[1].Color.ToString());
            //    vctFaces[1].Color = 66;
            //    vctFaces[1].face.RedisplayObject();
            //    VctFace face2 = new VctFace(vctFaces[1].face);
            //    lw.WriteLine(face2.Color.ToString() + " == VctFace Color");
            //    lw.WriteLine(vctFaces[1].face.Color.ToString() + " == Face Color");
            //}
            //catch (NXException ex)
            //{
            //    UI.GetUI().NXMessageBox.Show("Error", NXMessageBox.DialogType.Error, ex.ToString());
            //}
        }
    }
}
